using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using Microsoft.Extensions.CommandLineUtils;
using Netdx.ConversationTracker;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.IO.Compression;
using Cassandra.Data.Linq;
using Flowify.Cassandra;
using Cassandra.Mapping;

namespace Flowify
{
    class TrackFlows
    {
        
        public static string Name => "track-flows";
        public static Action<CommandLineApplication> Configuration =>
            (CommandLineApplication target) =>
            {
                target.Description = "Track flows and write flow records to the specified output.";

                var inputFile = target.Option("-r", "Read packet data from infile, can be any supported capture file format (including gzipped files).", CommandOptionType.SingleValue);
                var captureInterface = target.Option("-i", "Set the name of the network interface or pipe to use for live packet capture.", CommandOptionType.SingleValue);
                var jsonOutput = target.Option("-j", "Specifies output JSON file to store flow records.", CommandOptionType.SingleValue);
                var flowOutput = target.Option("-f", "Specifies folder where to store individual pcaps of flows", CommandOptionType.SingleValue);
                var combinedFlowOutput = target.Option("-g", "Specifies folder where to store individual pcaps of flows", CommandOptionType.SingleValue);

                ICaptureDevice GetInputDevice()
                {
                    if (!inputFile.HasValue() && !captureInterface.HasValue())
                    {
                        throw new ArgumentException("Either input file (-r <infile>) or capture interface (-i <capint>) must be specified.");
                    }
                    ICaptureDevice inputDevice = null;
                    if (inputFile.HasValue())
                    {
                        inputDevice = new SharpPcap.LibPcap.CaptureFileReaderDevice(inputFile.Value());
                        linkLayers = inputDevice.LinkType;
                    }
                    if (captureInterface.HasValue())
                    {
                        if (Int32.TryParse(captureInterface.Value(), out var interfaceIndex))
                        {
                            if (interfaceIndex < CaptureDeviceList.Instance.Count)
                            {
                                inputDevice = CaptureDeviceList.Instance[interfaceIndex];
                                linkLayers = inputDevice.LinkType;
                            }
                            else
                            {
                                throw new ArgumentException($"Interface index: {captureInterface.Value()} is invalid. This value should be between 0 and {CaptureDeviceList.Instance.Count - 1}. Use print-interfaces command to see available options.");
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid interface index: {captureInterface.Value()}. This should be an integer value between 0 and {CaptureDeviceList.Instance.Count - 1}. Use print-interfaces command to see available options.");
                        }
                    }
                    return inputDevice;
                }

                target.OnExecute(() =>
                {
                    var device = GetInputDevice();
                    var table = new FlowTable();
                    var index = new FlowIndex(1000, 100, (int)device.LinkType);
                    var tracker = new Tracker<(Packet, PosixTimeval), FlowKey, FlowRecordWithPackets>(table, table, table);
                    var captureTsc = new TaskCompletionSource<CaptureStoppedEventStatus>();
                    var captureTask = captureTsc.Task;
                    var packetOffset = 6 * sizeof(uint);
                    var packetCount = 0;
                    device.OnPacketArrival += Device_OnPacketArrival;
                    device.OnCaptureStopped += Device_OnCaptureStopped;
                    void Device_OnPacketArrival(object sender, CaptureEventArgs e)
                    {
                        var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
                        var flowRecord = tracker.UpdateFlow((packet, e.Packet.Timeval), out var flowKey);
                        index.Add(++packetCount, packetOffset, flowKey);
                        packetOffset += e.Packet.Data.Length + 4 * sizeof(uint);
                    }
                    void Device_OnCaptureStopped(object sender, CaptureStoppedEventStatus status)
                    {
                        captureTsc.SetResult(status);
                    }

                    device.Open();
                    device.Capture();
                    // following will block until capture is completed. 
                    var result = captureTask.Result;
                    device.Close();
                   
                    
                    // STORE DATA:
                    if (jsonOutput.HasValue())
                    {
                        SaveToJson(jsonOutput.Value(), table);
                    }
                    if (flowOutput.HasValue())
                    {
                        var path = Path.GetFullPath(flowOutput.Value());
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        SaveFlows(path, table);
                    }
                    if (combinedFlowOutput.HasValue())
                    {
                        var path = Path.GetFullPath(combinedFlowOutput.Value());
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        SaveCombinedFlows(path, table);
                    }
                    return 0;
                });
            };

        public static LinkLayers linkLayers { get; private set; }

        /// <summary>
        /// Saves content of each flow to the specified folder. 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="table"></param>
        private static void SaveFlows(string folder, FlowTable table)
        {
            foreach(var (flow, index) in table.Entries.Select((x, i) => (x, i+1)))
            {
                var path = Path.Combine(folder, index.ToString())+ ".pcap";
                
                var pcapfile = new CaptureFileWriterDevice(path);
                foreach (var (packet, time) in flow.Value.PacketList)
                {
                    pcapfile.Write(new RawCapture(linkLayers, time, packet.Bytes));                            
                }
            }
        }

        private static Process ExecuteTshark(string pcapPath, string jsonPath)
        {
            var pProcess = new Process();
            pProcess.StartInfo.FileName = @"C:\Program Files\Wireshark\tshark.exe";
            pProcess.StartInfo.Arguments = $"-r {pcapPath} -T json";
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pProcess.StartInfo.CreateNoWindow = true; //not diplay a windows
            pProcess.Start();
            return pProcess;
        }

        private static  void SaveCombinedFlows(string folder, FlowTable table)
        {

            var indexedFlows = table.Entries.Select((flow, i) => (flow, i + 1));
            Parallel.ForEach(indexedFlows, WriteFlow(folder));
        }

        private static Action<(KeyValuePair<FlowKey, FlowRecordWithPackets> flow, int)> WriteFlow(string folder)
        {
            return (flowIndex) =>
            {
                var flow = flowIndex.flow;
                var index = flowIndex.Item2;
                var path = Path.Combine(folder, index.ToString()) + ".pcap";
                var jsonPath = Path.ChangeExtension(path, ".json");
                var pcapfile = new CaptureFileWriterDevice(path);
                foreach (var (packet, time) in flow.Value.PacketList)
                {
                    pcapfile.Write(new RawCapture(linkLayers, time, packet.Bytes));
                }
                pcapfile.Close();

                var process = ExecuteTshark(path, jsonPath);

                using (var compressedFileStream = File.Create(jsonPath + ".gz"))
                using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                {
                    process.StandardOutput.BaseStream.CopyTo(compressionStream);
                }                
                process.WaitForExit();
            };
        }

        /// <summary>
        /// Saves flow table to JSON output file.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="table"></param>
        private static void SaveToJson(string filename, FlowTable table)
        {
            using (var file = File.CreateText(filename))
            using (var writer = new JsonTextWriter(file))
            {
                writer.WriteStartArray();
                foreach (var (flow, index) in table.Entries.Select((x,i) => (x,i+1)))
                {
                    var jObject = new JObject
                    {
                        { "Id", index },
                        { "Protocol", flow.Key.Protocol.ToString() },
                        { "SourceAddress", flow.Key.SourceEndpoint.Address.ToString() },
                        { "SourcePort", flow.Key.SourceEndpoint.Port },
                        { "DestinationAddress", flow.Key.DestinationEndpoint.Address.ToString() },
                        { "DestinationPort", flow.Key.DestinationEndpoint.Port },
                        { "FirstSeen", flow.Value.FirstSeen },
                        { "LastSeen", flow.Value.LastSeen },
                        { "Octets", flow.Value.Octets },
                        { "Packets", flow.Value.Packets }
                    };
                    jObject.WriteTo(writer);
                }
                writer.WriteEndArray();
            }
        }

        void PrintTable(Stopwatch sw, FlowTable table, int packets, int parserErrors)
        {
            Console.Clear();
            Console.WriteLine($"Time:    {sw.Elapsed}");
            Console.WriteLine($"Packets: {packets}   ");
            Console.WriteLine($"Flows:   {table.Count} ");
            Console.WriteLine($"Errors:  {parserErrors}");
            Console.WriteLine();
            try
            {
                table.Enter();
                var top = from t in table.Entries
                          orderby t.Value.Octets descending
                          select t;

                Console.WriteLine($"Proto | Source                   | Destination              | Pckts |    Octets |    Start |  Duration |");
                foreach (var flow in top.Take(10))
                {
                    Console.WriteLine($"{flow.Key.Protocol,5} | {flow.Key.SourceEndpoint,-24} | {flow.Key.DestinationEndpoint,-24} | {flow.Value.Packets,5} | {flow.Value.Octets,9} | {flow.Value.FirstSeen,9} | {flow.Value.LastSeen - flow.Value.FirstSeen,9} |");
                }
            }
            finally { table.Exit(); }
        }

  
    }
}


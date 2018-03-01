using System;
using System.Diagnostics;
using System.IO;
using Netdx.ConversationTracker;
using PacketDotNet;
using SharpPcap.LibPcap;
using SharpPcap;
using System.Linq;
using System.Threading;

namespace flowstat
{
    /// <summary>
    /// Computes the flow statististics for the given input pcap file.
    /// It also creates a hierarchy of bloom filters to improve the access to individual packets. 
    /// </summary>
    class Program
    {
        private static Tracker<(Packet, PosixTimeval), FlowKey, FlowRecord> m_tracker;
        private static int m_parserError;
        private static int m_packets;

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("No input file specified!");
                return;
            }
            var inputFile = args[0];
            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine("Specified input file not find!");
                return;
            }
            var sw = new Stopwatch();
            sw.Start();

            var device = new CaptureFileReaderDevice(inputFile);
            device.Open();

            var table = new FlowTable();
            var index = new FlowIndex(1000, 100, (int)device.LinkType);
            m_tracker = new Tracker<(Packet,PosixTimeval), FlowKey, FlowRecord>(table, table, table);

            void PrintTable()
            {
                Console.Clear();
                Console.WriteLine($"Time:    {sw.Elapsed}");
                Console.WriteLine($"Packets: {m_packets}   ");
                Console.WriteLine($"Flows:   {table.Count} ");
                Console.WriteLine($"Errors:  {m_parserError}");
                Console.WriteLine();
                // print top 10 flows
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
                finally  { table.Exit(); }
            }

            PeriodicTask.Run(PrintTable, new TimeSpan(0, 0, 1));
            PrintTable();
            var rawCapture = device.GetNextPacket();
            var packetOffset = 6 * sizeof(uint);
            while (rawCapture != null)
            {
                try
                {
                    
                    m_packets++;
                    var packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
                    m_tracker.UpdateFlow((packet, rawCapture.Timeval));
                    var packetFlowKey = table.GetKey((packet, rawCapture.Timeval));
                    index.Add(m_packets, packetOffset, packetFlowKey);
                    packetOffset += rawCapture.Data.Length + 4 * sizeof(uint);
                }
                catch (Exception)
                {
                    m_parserError++;
                }
                
                rawCapture = device.GetNextPacket();
                
            }

            device.Close();
            sw.Stop();
            PrintTable();
        }
    }
}

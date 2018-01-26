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
    /// </summary>
    class Program
    {
        private static Tracker<(Packet, PosixTimeval), FlowKey, FlowRecord> m_tracker;
        private static int parserError;
        private static int packets;

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

            var table = new FlowTable();
            m_tracker = new Tracker<(Packet,PosixTimeval), FlowKey, FlowRecord>(table, table, table);

            var device = new CaptureFileReaderDevice(inputFile);
            device.Open();


            void PrintTable()
            {
                Console.Clear();
                Console.WriteLine($"Time:    {sw.Elapsed}");
                Console.WriteLine($"Packets: {packets}   ");
                Console.WriteLine($"Flows:   {table.Count} ");
                Console.WriteLine($"Errors:  {parserError}");
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
            while (rawCapture != null)
            {
                try
                {
                    packets++;
                    var packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
                    m_tracker.UpdateFlow((packet, rawCapture.Timeval));
                }
                catch (Exception)
                {
                    parserError++;
                }
                rawCapture = device.GetNextPacket();
            }

            device.Close();
            sw.Stop();
            PrintTable();
        }
    }
}

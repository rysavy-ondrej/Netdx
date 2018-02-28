using Kaitai;
using Microsoft.Extensions.CommandLineUtils;
using Netdx.Packets.IoT;
using PacketDotNet;
using SharpPcap;
using System;
using System.IO;
using System.Linq;

namespace lwm2mdump
{
    /// <summary>
    /// Dumps the events as simple text output.
    /// </summary>
    class DumpEvents
    {
        internal static readonly string Name = "dump-events";

        public static Action<CommandLineApplication> Configuration =>
            (CommandLineApplication target) =>
            {

                var debug = target.Option("-d", "Enable debug mode.", CommandOptionType.NoValue);
                var inputFile = target.Option("-r", "Read packet data from infile, can be any supported capture file format (including gzipped files).", CommandOptionType.SingleValue);
                var captureInterface = target.Option("-i", "Set the name of the network interface or pipe to use for live packet capture.", CommandOptionType.SingleValue);
                var outputFormat = target.Option("-T", "Set the format of the output when viewing decoded packet data.", CommandOptionType.SingleValue);

                target.Description = "Dumps LwM2M events to simple text output format.";
                target.OnExecute(() =>
                {
                    if (!inputFile.HasValue() && !captureInterface.HasValue())
                    {
                        throw new ArgumentException("Either input file (-r <infile>) or capture interface (-i <capint>) must be specified.");
                    }
                    ICaptureDevice inputDevice = null;
                    if (inputFile.HasValue())
                    {
                        inputDevice = new SharpPcap.LibPcap.CaptureFileReaderDevice(inputFile.Value());
                    }
                    if (captureInterface.HasValue())
                    {
                        if (Int32.TryParse(captureInterface.Value(), out int interfaceIndex))
                        {
                            if (interfaceIndex < CaptureDeviceList.Instance.Count)
                            {
                                inputDevice = CaptureDeviceList.Instance[interfaceIndex];
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

                    IOutputFormatter outputFormatter = null;
                    switch (outputFormat.Value() ?? "text")
                    {
                        case "json": outputFormatter = new JsonOutputFormatter(); break;
                        case "csv": outputFormatter = new CsvOutputFormatter(); break;
                        case "text": outputFormatter = new TextOutputFormatter(); break;
                        default: throw new ArgumentException($"Unknown output format  {outputFormat.Value()}. Available formats are json|csv|text.");
                    }

                    Console.WriteLine($"Processing {inputDevice.Description} -> {outputFormatter}");
                    Execute(inputDevice, outputFormatter);
                    return 0;
                });
            };

        public static void Execute(ICaptureDevice device, IOutputFormatter output)
        {
            device.OnPacketArrival += Device_OnPacketArrival;
            device.Open();
            // enable processing only udp packets as CoAP is carried in UDP.
            device.Filter = "ip and udp";
            device.Capture();
            device.Close();
        }


        private static void Device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            try
            {
                var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
                var ip = packet.Extract(typeof(IpPacket)) as IpPacket;
                var udp = packet.Extract(typeof(UdpPacket)) as UdpPacket;
                if (Coap.IsCoap(udp.PayloadData))
                {
                    File.WriteAllBytes($"{e.Device.Statistics.ReceivedPackets:0000}.raw", udp.PayloadData);

                    var coap = new Coap(new KaitaiStream(udp.PayloadData));
                    var uri = coap.GetUri(ip.DestinationAddress.ToString(), udp.DestinationPort);
                    var parameters = System.Web.HttpUtility.ParseQueryString(uri.Query);
                    Console.WriteLine($"{e.Packet.Timeval.Date}: {packet}[CoAPPacket: Code={coap.Code}, Type={coap.Type}, MID={coap.MessageId}, Uri={uri}]");
                    // analyze CoAP message to generate LwM2M event:
                    if (coap.IsRequest && coap.RequestMethod == RequestMethod.Post && uri.LocalPath.Equals("/rd"))
                    {
                        Console.WriteLine($"EVENT: [LwM2M.Register: endpoint={parameters["ep"]}, lifetime={parameters["lt"]}, version={parameters["lwm2m"]}, binding={parameters["b"]}]");   
                    }
                }
            }
            catch(Exception)
            {
                Console.Error.WriteLine($"{e.Packet.Timeval.Date}: Unable to parse packet.");
            }
        }
    }
}

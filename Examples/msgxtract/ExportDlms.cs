using Kaitai;
using Microsoft.Extensions.CommandLineUtils;
using Netdx.Packets.Industrial;
using PacketDotNet;
using SharpPcap;
using System;
using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;
using static Netdx.Packets.Industrial.DlmsStruct;

namespace Netdx.Examples.MessageExtract
{

    static class DisplayStringExtension
    {
        public static string ToDisplayString(this byte[] x)
        {
            return BitConverter.ToString(x);
        }
        public static string ToDisplayString(this byte x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this sbyte x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this ushort x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this short x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this uint x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this int x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this ulong x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this long x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this bool x)
        {
            return $"{(x ? "1" : "0")} ({x.ToString()})";
        }
        public static string ToDisplayString(this float x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this double x)
        {
            return $"0x{x.ToString("X")} ({x.ToString()})";
        }
        public static string ToDisplayString(this Enum x, int size=1)
        {
            var i = x.ToString("X");
            var str = i.Substring(i.Length - size*2);
            return $"0x{str} ({x.ToString()})";
        }

        public static string ToDisplayString(this CosemObjectInstanceId x)
        {
            return String.Join('.',x.Oid.Select(b => b.ToString()));
        }

        public static YamlNode ToYaml(this CosemAttributeDescriptor cosemAttributeDescriptor)
        {
            return new YamlMappingNode
            {
                { "class-id", cosemAttributeDescriptor.ClassId.ToDisplayString() },
                { "OBIS-code", cosemAttributeDescriptor.InstanceId.ToDisplayString() },
                { "attribute-id", cosemAttributeDescriptor.AttributeId.ToDisplayString() }
            };
        }

        public static YamlNode ToYaml(this GetDataResult dataResult)
        {
            switch(dataResult.DataResultValue)
            {
                case DlmsData data:
                    return new YamlMappingNode
                    {
                        { "Data", data.ToYaml() }
                    };
                case DataAccessResult access:
                    return new YamlMappingNode
                    {
                        { "Access-Result", access.Value.ToDisplayString() }
                    };
                default:
                    return new YamlMappingNode();
            }
        }
        public static YamlNode ToYaml(this DlmsData data)
        {
            var yaml = new YamlMappingNode
            {
                { "type", data.Datatype.ToDisplayString() }
            };
            switch (data.DataValue)
            {
                case DlmsData.Array _array:
                    yaml.Add("value", _array.Value.ToDisplayString());
                    break;
                case DlmsData.Structure _structure:
                    break;
                case DlmsData.OctetString _octets:
                    yaml.Add("value", _octets.Value.ToDisplayString());
                    break;                
                case DlmsData.Boolean _boolean:
                    yaml.Add("value", _boolean.Value.ToDisplayString());
                    break;
                case DlmsData.Enum _enum:
                    yaml.Add("value", _enum.Value.ToDisplayString());
                    break;
                case DlmsData.VisibleString _visibleString:
                    yaml.Add("value", _visibleString.Value);
                    break;

                case DlmsData.Integer _integer:
                    yaml.Add("value", _integer.Value.ToDisplayString());
                    break;
                case DlmsData.Long _integer:
                    yaml.Add("value", _integer.Value.ToDisplayString());
                    break;
                case DlmsData.Unsigned _integer:
                    yaml.Add("value", _integer.Value.ToDisplayString());
                    break;
                case DlmsData.LongUnsigned _integer:
                    yaml.Add("value", _integer.Value.ToDisplayString());
                    break;
                case DlmsData.DoubleLong _integer:
                    yaml.Add("value", _integer.Value.ToDisplayString());
                    break;
                case DlmsData.DoubleLongUnsigned _integer:
                    yaml.Add("value", _integer.Value.ToDisplayString());
                    break;
                case DlmsData.Float32 _float:
                    yaml.Add("value", _float.Value.ToDisplayString());
                    break;
                case DlmsData.Float64 _float:
                    yaml.Add("value", _float.Value.ToDisplayString());
                    break;
                case DlmsData.Long64 _integer:
                    yaml.Add("value", _integer.Value.ToDisplayString());
                    break;
                case DlmsData.Long64Unsigned _integer:
                    yaml.Add("value", _integer.Value.ToDisplayString());
                    break;
            }
            return yaml;
        }
    }

    /// <summary>
    /// Dumps the events as simple text output.
    /// </summary>
    class ExportDlms
    {
        internal static readonly string Name = "export-dlms";

        public static Action<CommandLineApplication> Configuration =>
            (CommandLineApplication target) =>
            {
                var debug = target.Option("-d", "Enable debug mode.", CommandOptionType.NoValue);
                var inputFile = target.Option("-r", "Read packet data from infile, can be any supported capture file format (including gzipped files).", CommandOptionType.SingleValue);
                var captureInterface = target.Option("-i", "Set the name of the network interface or pipe to use for live packet capture.", CommandOptionType.SingleValue);
                var outputFolder = target.Option("-w", "Write the output to the specified folder.", CommandOptionType.SingleValue);
                target.Description = "Exports raw DLMS message and description in YAML format.";
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
                    var executor = new ExportDlms()
                    {
                        OutputPath = (outputFolder.HasValue()) ? outputFolder.Value() : Directory.GetCurrentDirectory()
                    };
                    
                    Console.WriteLine($"Processing {inputDevice.Description} -> {executor.OutputPath}");
                    executor.Execute(inputDevice);
                    return 0;
                });
            };
        internal string OutputPath { get; set; }
        public void Execute(ICaptureDevice device)
        {
            device.OnPacketArrival += Device_OnPacketArrival;
            device.Open();
            device.Filter = "tcp port 4061";
            device.Capture();
            device.Close();
        }

        int packetNumber;
        private void Device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            try
            {
                var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
                packetNumber++;
                var ip = packet.Extract(typeof(IpPacket)) as IpPacket;
                var tcp = packet.Extract(typeof(TcpPacket)) as TcpPacket;

                if (tcp != null &&
                    tcp.PayloadData.FirstOrDefault() == 0x7e &&
                    tcp.PayloadData.LastOrDefault() == 0x7e)
                {
                    // write binary data
                    var rawfilename = Path.Combine(this.OutputPath, $"packet_{packetNumber.ToString("0000")}.raw");
                    var yamlfilename = Path.Combine(this.OutputPath, $"packet_{packetNumber.ToString("0000")}.yaml");
                    File.WriteAllBytes(rawfilename, tcp.PayloadData);



                    var yamlDlms = new YamlMappingNode();
                    

                    var str = new KaitaiStream(tcp.PayloadData);
                    var hdlc = new DlmsHdlc(str);
                    var yamlHdlc = new YamlMappingNode
                    {
                        { "Frame Format Type", hdlc.HdlcHeader.Format.FrameFormatType.ToDisplayString() },
                        { "Segmentation Flag", hdlc.HdlcHeader.Format.SegmentationFlag.ToDisplayString() },
                        { "HDLC Frame Length", hdlc.HdlcHeader.Format.FrameLength.ToDisplayString() },
                        { "Destination Address", hdlc.HdlcHeader.DstAddress.Value.ToDisplayString() },
                        { "Source Address", hdlc.HdlcHeader.SrcAddress.Value.ToDisplayString() },
                        { "Control Field", hdlc.HdlcHeader.Control.ControlByte.ToDisplayString() },
                        { "Header Checksum", hdlc.HdlcHeader.Hcs.ToDisplayString() },
                        { "Frame Checksum", hdlc.Fsc.ToDisplayString() }
                    };
                    if (hdlc.LlcHeader != null)
                    {
                        var yamlLlc = new YamlMappingNode
                        {
                            { "Destinaton LSAP", hdlc.LlcHeader.RemoteLsap?.ToDisplayString() },
                            { "Source LSAP", hdlc.LlcHeader.LocalLsap.ToDisplayString() }
                        };
                        yamlHdlc.Add("LLC", yamlLlc);
                    }
                    yamlHdlc.Add("Information", hdlc.Information.ToDisplayString());
                    yamlDlms.Add("Hdlc", yamlHdlc);
                   
                    var dlmsType = hdlc.Information.FirstOrDefault();
                    if (dlmsType >= 192 && dlmsType <= 199)
                    {
                        var pdu = new DlmsApdu(new KaitaiStream(hdlc.Information));
                        var yamlPdu = new YamlMappingNode
                        {
                            { "Type", pdu.PduType.ToDisplayString() }
                        };
                        // export pdu info
                        switch (pdu.Pdu)
                        {
                            case DlmsGetRequest getRequestPdu:
                                yamlPdu.Add("GetRequest", getRequestPdu.RequestType.ToDisplayString());
                                switch(getRequestPdu.Request)
                                {
                                    case DlmsGetRequest.GetRequestNormal getRequestNormal:
                                        yamlPdu.Add("Invoke ID", getRequestNormal.InvokeIdAndPriority.InvokeId.ToDisplayString());
                                        yamlPdu.Add("Priority", getRequestNormal.InvokeIdAndPriority.Priority.ToDisplayString());
                                        yamlPdu.Add("Cose-Attribute-Descriptor", getRequestNormal.CosemAttributeDescriptor.ToYaml());
                                        break;
                                    case DlmsGetRequest.GetRequestNext getRequestNext:
                                        yamlPdu.Add("Invoke ID", getRequestNext.InvokeIdAndPriority.InvokeId.ToDisplayString());
                                        yamlPdu.Add("Priority", getRequestNext.InvokeIdAndPriority.Priority.ToDisplayString());
                                        yamlPdu.Add("block-number", getRequestNext.BlockNumber.ToDisplayString());
                                        break;
                                    case DlmsGetRequest.GetRequestWithList getRequestWithList:
                                        yamlPdu.Add("Invoke ID", getRequestWithList.InvokeIdAndPriority.InvokeId.ToDisplayString());
                                        yamlPdu.Add("Priority", getRequestWithList.InvokeIdAndPriority.Priority.ToDisplayString());
                                        yamlPdu.Add("Attribute-list", new YamlSequenceNode(getRequestWithList.AttributeDescriptorList.Items.Select(c => c.CosemAttributeDescriptor.ToYaml())));
                                        break;
                                }
                                break;
                            case DlmsGetResponse getResponsePdu:
                                yamlPdu.Add("GetResponse", getResponsePdu.ResponseType.ToDisplayString());
                                switch(getResponsePdu.Response)
                                {
                                    case DlmsGetResponse.GetResponseNormal getResponseNormal:
                                        yamlPdu.Add("Invoke ID", getResponseNormal.InvokeIdAndPriority.InvokeId.ToDisplayString());
                                        yamlPdu.Add("Priority", getResponseNormal.InvokeIdAndPriority.Priority.ToDisplayString());
                                        yamlPdu.Add("Result", getResponseNormal.Result.ToYaml());
                                        break;
                                    case DlmsGetResponse.GetResponseWithList getResponseWithList:
                                        yamlPdu.Add("Invoke ID", getResponseWithList.InvokeIdAndPriority.InvokeId.ToDisplayString());
                                        yamlPdu.Add("Priority", getResponseWithList.InvokeIdAndPriority.Priority.ToDisplayString());
                                        yamlPdu.Add("Results", new YamlSequenceNode(getResponseWithList.Result.Items.Select(c => c.ToYaml())));
                                        break;
                                    case DlmsGetResponse.GetResponseWithDatablock getResponseWithDatablock:
                                        yamlPdu.Add("Invoke ID", getResponseWithDatablock.InvokeIdAndPriority.InvokeId.ToDisplayString());
                                        yamlPdu.Add("Priority", getResponseWithDatablock.InvokeIdAndPriority.Priority.ToDisplayString());
                                        break;
                                }
                                break;
                            case DlmsSetRequest setRequestPdu:
                                yamlPdu.Add("SetRequest", setRequestPdu.RequestType.ToDisplayString());
                                switch(setRequestPdu.Request)
                                {
                                    case DlmsSetRequest.SetRequestNormal setRequestNormal:
                                        yamlPdu.Add("Invoke ID", setRequestNormal.InvokeIdAndPriority.InvokeId.ToDisplayString());
                                        yamlPdu.Add("Priority", setRequestNormal.InvokeIdAndPriority.Priority.ToDisplayString());
                                        yamlPdu.Add("Cose-Attribute-Descriptor", setRequestNormal.CosemAttributeDescriptor.ToYaml());
                                        yamlPdu.Add("Value", setRequestNormal.Value.ToYaml());
                                        break;
                                    case DlmsSetRequest.SetRequestWithList setRequestWithList:
                                        yamlPdu.Add("Invoke ID", setRequestWithList.InvokeIdAndPriority.InvokeId.ToDisplayString());
                                        yamlPdu.Add("Priority", setRequestWithList.InvokeIdAndPriority.Priority.ToDisplayString());
                                        yamlPdu.Add("Cose-Attribute-Descriptor", new YamlSequenceNode(setRequestWithList.AttributeDescriptorList.Items.Select(c => c.CosemAttributeDescriptor.ToYaml())));
                                        yamlPdu.Add("Value", new YamlSequenceNode(setRequestWithList.ValueList.Items.Select(c => c.ToYaml())));
                                        break;
                                    case DlmsSetRequest.SetRequestWithDatablock setRequestWithDatablock:
                                        throw new NotImplementedException();
                                    case DlmsSetRequest.SetRequestWithFirstDatablock setRequestWithFirstDatablock:
                                        throw new NotImplementedException();
                                    case DlmsSetRequest.SetRequestWithListAndFirstDatablock setRequestWithListAndFirstDatablock:
                                        throw new NotImplementedException();
                                }
                                break;
                            default:
                                break;
                        }
                        yamlDlms.Add("Dlms/Cosem", yamlPdu);
                    }
                    else if(dlmsType >= 96 && dlmsType <=101)
                    {
                        var acse = new DlmsAcse(new KaitaiStream(hdlc.Information));
                        var yamlAcse = new YamlMappingNode();
                        // export info
                        yamlDlms.Add("Dlms/Cosem", yamlAcse);
                    }
                    
                    var yamlDoc = new YamlDocument(yamlDlms);                    
                    using (var yamlwriter = new StreamWriter(yamlfilename))
                    {
                        var yamlStream = new YamlStream(yamlDoc);
                        yamlStream.Save(yamlwriter);
                    }                   
                }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"{e.Packet.Timeval.Date}: Unable to parse packet.");
            }
        }




    }
}

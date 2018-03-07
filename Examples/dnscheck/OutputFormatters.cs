using Netdx.ConversationTracker;
using Netdx.Packets;
using Netdx.Packets.Core;
using SharpPcap;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Netdx.Examples.DnsCheck
{
    internal interface IOutputFormatter
    {
        /// <summary>
        /// Writes information about the <see cref="DnsPacket"/> to the output. 
        /// </summary>
        /// <param name="packet"></param>
        void WriteRecord(PosixTimeval timestamp, FlowKey flowKey, DnsPacket packet);        
    }


    internal class TextOutputFormatter : IOutputFormatter, IDisposable
    {
        StreamWriter m_writer;
        internal TextOutputFormatter(Stream stream)
        {
            m_writer = new StreamWriter(stream);
        }

        public void Dispose()
        {
            ((IDisposable)m_writer).Dispose();
        }

        /// <summary>
        /// Write the text representation of <see cref="DnsPacket"/>. It uses Flix assert format, e.g., Frame(DnsPacket(0.1, 1234, Query)).
        /// </summary>
        /// <param name="packet"></param>
        public void WriteRecord(PosixTimeval timestamp, FlowKey flowKey, DnsPacket packet)
        {
            string answerString(DnsPacket.Answer answer)
            {

                switch (answer.Rdata)
                {
                    case DnsPacket.ARecord a:
                        return $"A(\"{answer.Name.DomainNameString}\",\"{new IPAddress(a.Address)}\")";
                    case DnsPacket.AaaaRecord aaaa:     
                        return $"AAAA(\"{answer.Name.DomainNameString}\",\"{new IPAddress(aaaa.Address)}\")";
                    case DnsPacket.CnameRecord cname:
                        return $"CNAME(\"{answer.Name.DomainNameString}\",\"{cname.Hostname.DomainNameString}\")";
                    case DnsPacket.NsRecord ns:
                        return $"NS(\"{answer.Name.DomainNameString}\",\"{ns.Hostname.DomainNameString}\")";
                    case DnsPacket.MxRecord mx:
                        return $"NS(\"{answer.Name.DomainNameString}\",{mx.Priority},\"{mx.Hostname.DomainNameString}\")";
                    default:
                        return "NULL";
                }
            }

            var ts = ((double)timestamp.Seconds + ((double)timestamp.MicroSeconds)/1000000).ToString("0.000");
            var id = packet.TransactionId;
            var rcode = packet.Flags.Rcode == 0 ? "NoError" : "NameDoesNotExist";
           
            var queries = String.Join("::", packet.Queries.Select(x => $"{x.Type.ToString().ToUpperInvariant()}(\"{x.Name.DomainNameString}\",\"\")").Append("Nil"));
            var answers = String.Join("::", packet.Answers.Select(answerString).Append("Nil"));            
            var qr = packet.Flags.Qr == 0 ? $"Query({id},{queries})" : $"Response({id},{rcode},{queries},{answers})";
            var dns = $"DNS({qr})";

            
            var proto = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(flowKey.Protocol.ToString());
            var ipSrc = flowKey.SourceEndpoint.Address.ToString();
            var ipDst = flowKey.DestinationEndpoint.Address.ToString();
            var portSrc = flowKey.SourceEndpoint.Port;
            var portDst = flowKey.DestinationEndpoint.Port;
            var flow = $"{proto}(\"{ipSrc}\",\"{ipDst}\",{portSrc},{portDst})";

            // Frame(1520329507.498, Udp("192.168.111.100", "147.229.9.43", 1234, 53), DnsPacket(Query(15595,A("api.github.com.", "")::Nil))).
            var str = $"Frame({ts}, {flow}, {dns}).";
            m_writer.WriteLine(str);
            m_writer.Flush();
        }
    }
}
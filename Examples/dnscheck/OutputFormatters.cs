using Netdx.Packets;
using Netdx.Packets.Core;
using SharpPcap;
using System;
using System.IO;
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
        void WriteRecord(PosixTimeval timestamp, IPAddress source, IPAddress destination, DnsPacket packet);        
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
        public void WriteRecord(PosixTimeval timestamp, IPAddress source, IPAddress destination, DnsPacket packet)
        {
            var ts = ((double)timestamp.Seconds + ((double)timestamp.MicroSeconds)/1000000).ToString("0.000");
            var id = packet.TransactionId;
            var src = source.ToString();
            var trg = destination.ToString();
            var rcode = packet.Flags.Rcode == 0 ? "NoError" : "NameDoesNotExist";
            var qr = packet.Flags.Qr == 0 ? "Query" : $"Response({rcode})";
            var str = $"Frame(DnsPacket({ts}, \"{src}\", \"{trg}\", {id}, {qr})).";
            m_writer.WriteLine(str);
            m_writer.Flush();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Netdx.Packets.Base;
using SharpPcap;
using SharpPcap.LibPcap;
using Xunit;

namespace PacketDecodersTests
{

    public class FastDecodeTest
    {
        [Theory]
        [InlineData("")]
        public void LoadAndParsePacket(string filename)
        {
            var packets = LoadPackets(filename);
            foreach (var packet in packets)
            {
                var bytes = packet.Data;
                var etherType = EthernetFrame.GetEtherType(bytes);
                var etherPayload = EthernetFrame.GetPayloadBytes(bytes);
                Span<Byte> ipPayload;
                Span<Byte> sourceAddress;
                Span<Byte> destinAddress;
                switch(etherType)
                {
                    case (ushort)EthernetFrame.EtherTypeEnum.Ipv4:
                        {
                            sourceAddress = Ipv4Packet.GetSourceAddress(etherPayload);
                            destinAddress = Ipv4Packet.GetDestinationAddress(etherPayload);
                            ipPayload = Ipv4Packet.GetPayloadBytes(etherPayload);
                            break;
                        }
                    case (ushort)EthernetFrame.EtherTypeEnum.Ipv6:
                        {
                            sourceAddress = Ipv6Packet.GetSourceAddress(etherPayload);
                            destinAddress = Ipv6Packet.GetDestinationAddress(etherPayload);
                            ipPayload = Ipv6Packet.GetPayloadBytes(etherPayload);
                            break;
                        }
                }
            }
        }

        private IList<RawCapture> LoadPackets(string filename)
        {
            var packetList = new List<RawCapture>();
            var device = new CaptureFileReaderDevice(filename);
            device.Open();
            RawCapture packet;
            while ((packet = device.GetNextPacket()) != null)
            {
                packetList.Add(packet);
            }
            device.Close();
            return packetList;
        }
    }
}

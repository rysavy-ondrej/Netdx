using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace Netdx.PacketDecoders.Base
{
    public partial class UdpDatagram
    {
         
        static class UdpFields
        {
            /// <summary> Length of a UDP port in bytes.</summary>
            public static readonly Int32 PortLength = 2;

            /// <summary> Length of the header length field in bytes.</summary>
            public static readonly Int32 HeaderLengthLength = 2;

            /// <summary> Length of the checksum field in bytes.</summary>
            public static readonly Int32 ChecksumLength = 2;

            /// <summary> Position of the source port.</summary>
            public static readonly Int32 SourcePortPosition = 0;

            /// <summary> Position of the destination port.</summary>
            public static readonly Int32 DestinationPortPosition;

            /// <summary> Position of the header length.</summary>
            public static readonly Int32 HeaderLengthPosition;

            /// <summary> Position of the header checksum length.</summary>
            public static readonly Int32 ChecksumPosition;

            /// <summary> Length of a UDP header in bytes.</summary>
            public static readonly Int32 HeaderLength; // == 8

            static UdpFields()
            {
                DestinationPortPosition = PortLength;
                HeaderLengthPosition = DestinationPortPosition + PortLength;
                ChecksumPosition = HeaderLengthPosition + HeaderLengthLength;
                HeaderLength = ChecksumPosition + ChecksumLength;
            }
        }
        public static UInt16 SourcePort(Span<Byte> udpBytes)
        {
            var port = udpBytes.Slice(UdpFields.SourcePortPosition);
            return BinaryPrimitives.ReadUInt16BigEndian(port);
        }
        public static UInt16 DestinationPort(Span<Byte> udpBytes)
        {
            var port = udpBytes.Slice(UdpFields.DestinationPortPosition);
            return BinaryPrimitives.ReadUInt16BigEndian(port);
        }
        public static Span<Byte> PayloadBytes(Span<Byte> udpBytes)
        {
            return udpBytes.Slice(UdpFields.HeaderLength);
        }
    }
}

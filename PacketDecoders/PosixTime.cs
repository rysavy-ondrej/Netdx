using System;
using System.Collections.Generic;
using System.Text;

namespace Netdx.PacketDecoders
{
    public class PosixTime
    {
        readonly UInt32 m_seconds;
        readonly UInt32 m_micros;

        public uint Seconds { get => m_seconds;  }
        public uint MicroSeconds { get => m_micros;  }

        public PosixTime(uint seconds, uint micros)
        {
            m_seconds = seconds;
            m_micros = micros;
        }
        public long ToUnixTimeMilliseconds()
        {
            return (long)((this.Seconds * 1000) + (this.MicroSeconds / 1000));
        }

        internal static PosixTime FromUnixTimeMilliseconds(long v)
        {
            return new PosixTime((uint)(v / 1000), (uint)((v % 1000) * 1000));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Netdx.ConversationTracker;
using PacketDotNet;
using IPEndPoint = System.Net.IPEndPoint;
using SharpPcap;
using System.Threading;
using System.IO;

namespace flowstat
{
    class FlowTable : IFlowTable<FlowKey, FlowRecord>, IKeyProvider<FlowKey, (Packet, PosixTimeval)>, IRecordProvider<(Packet, PosixTimeval), FlowRecord>
    {
        Dictionary<FlowKey, FlowRecord> m_table = new Dictionary<FlowKey, FlowRecord>();

        public object Count => m_table.Count;

        public IEnumerable<KeyValuePair<FlowKey, FlowRecord>> Entries => m_table;

        public FlowRecord Delete(FlowKey key)
        {
            lock (LockObject)
            {
                m_table.Remove(key, out var record);

                return record;
            }
        }

        public bool Exists(FlowKey key)
        {
            return m_table.ContainsKey(key);
        }

        public void FlushAll()
        {
            lock (LockObject)
            {
                m_table.Clear();
            }
        }

        public FlowRecord Get(FlowKey key)
        {
            return m_table.GetValueOrDefault(key);
        }

        public FlowKey GetKey((Packet, PosixTimeval) capture)
        {
            var packet = capture.Item1;
            FlowKey GetUdpFlowKey(UdpPacket udp)
            {
                return new FlowKey()
                {
                    Protocol = ProtocolType.UDP,
                    SourceEndpoint = new IPEndPoint((udp.ParentPacket as IpPacket).SourceAddress, udp.SourcePort),
                    DestinationEndpoint = new IPEndPoint((udp.ParentPacket as IpPacket).DestinationAddress, udp.DestinationPort),
                };
            }
            FlowKey GetTcpFlowKey(TcpPacket tcp)
            {
                return new FlowKey()
                {
                    Protocol = ProtocolType.TCP,
                    SourceEndpoint = new IPEndPoint((tcp.ParentPacket as IpPacket).SourceAddress, tcp.SourcePort),
                    DestinationEndpoint = new IPEndPoint((tcp.ParentPacket as IpPacket).DestinationAddress, tcp.DestinationPort),
                };
            }
            FlowKey GetIpFlowKey(IpPacket ip)
            {
                return new FlowKey()
                {
                    Protocol = ip.Version == IpVersion.IPv4 ? ProtocolType.IP : ProtocolType.IPv6,
                    SourceEndpoint = new IPEndPoint(ip.SourceAddress, 0),
                    DestinationEndpoint = new IPEndPoint(ip.DestinationAddress, 0),
                };
            }

            switch ((TransportPacket)packet.Extract(typeof(TransportPacket)))
            {
                case UdpPacket udp: return GetUdpFlowKey(udp);
                case TcpPacket tcp: return GetTcpFlowKey(tcp);
                default:
                    switch ((InternetPacket)packet.Extract(typeof(InternetPacket)))
                    {
                        case IpPacket ip: return GetIpFlowKey(ip);
                        default: return FlowKey.None;

                    }
            }
        }

        private readonly object LockObject = new object();

        public void Exit()
        {
            Monitor.Exit(LockObject);    
        }

        public void Enter()
        {
            Monitor.Enter(LockObject);
        }

        public FlowRecord GetRecord((Packet, PosixTimeval) capture)
        {
            var packet = capture.Item1;
            var time = (long)capture.Item2.MicroSeconds;
            return new FlowRecord()
            {
                FirstSeen = time,
                LastSeen = time,
                Octets = packet.BytesHighPerformance.BytesLength,
                Packets = 1,
            };
        }

        public FlowRecord Merge(FlowKey key, FlowRecord value)
        {

            var stored = m_table.GetValueOrDefault(key);
            FlowRecord newValue;
            if (stored != null)
            {
                newValue = new FlowRecord()
                {
                    FirstSeen = Math.Min(stored.FirstSeen, value.FirstSeen),
                    LastSeen = Math.Max(stored.FirstSeen, value.FirstSeen),
                    Octets = stored.Octets + value.Octets,
                    Packets = stored.Packets + value.Packets
                };
            }
            else
            {
                newValue = value;
            }
            lock (LockObject)
            {
                return m_table[key] = newValue;
            }
        }

        public void Put(FlowKey key, FlowRecord value)
        {
            lock (LockObject)
            {
                m_table[key] = value;
            }
        }


        public void Write(Stream stream)
        {

        }

        public void Read(Stream stream)
        {
            
        }
    }
}

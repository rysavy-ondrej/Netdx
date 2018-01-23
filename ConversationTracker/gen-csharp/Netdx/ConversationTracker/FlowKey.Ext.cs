//  
// Copyright (c) BRNO UNIVERSITY OF TECHNOLOGY. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.  
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Netdx.ConversationTracker
{
    [DebuggerDisplay("[FlowKey: {IpProtocol} {SourceEndpoint} -> {DestinationEndpoint}]")]
    public partial class FlowKey : IEquatable<FlowKey>
    {

        private static readonly FlowKey m_none = new FlowKey() { Protocol = IpProtocolType.NONE, SourceEndpoint = new IPEndPoint(IPAddress.None, 0), DestinationEndpoint = new IPEndPoint(IPAddress.None, 0) };
        public static FlowKey None => m_none;

        /// <summary>
        /// Gets or sets Source as IPEndPoint
        /// </summary>
        public IPEndPoint SourceEndpoint
        {
            get
            {
                if (_SourcePoint == null)
                {
                    return new IPEndPoint(IPAddress.None, 0);
                }
                else
                {
                    
                    var len = _SourcePoint.Length;
                    switch(len)
                    {
                        case 8:
                            var ipv4 = new Span<byte>(_SourcePoint, 0, 4);
                            return new IPEndPoint(new IPAddress(ipv4.ToArray()), BitConverter.ToInt32(_SourcePoint, 4));
                        case 20:
                            var ipv6 = new Span<byte>(_SourcePoint, 0, 16);
                            return new IPEndPoint(new IPAddress(ipv6.ToArray()), BitConverter.ToInt32(_SourcePoint, 16));
                        default:
                            throw new InvalidOperationException("SourcePoint does not represent valid IPEndPoint.");
                    }
                }
            }
            set
            {
                // 4 or 16 bytes + 4 bytes
                _SourcePoint = value.Address.GetAddressBytes().Concat(BitConverter.GetBytes(value.Port)).ToArray();
            }
        }
        public IPEndPoint DestinationEndpoint
        {
            get
            {
                if (_DestinationPoint == null)
                {
                    return new IPEndPoint(IPAddress.None, 0);
                }
                else
                {

                    var len = _DestinationPoint.Length;
                    switch (len)
                    {
                        case 8:
                            var ipv4 = new Span<byte>(_DestinationPoint, 0, 4);
                            return new IPEndPoint(new IPAddress(ipv4.ToArray()), BitConverter.ToInt32(_SourcePoint, 4));
                        case 20:
                            var ipv6 = new Span<byte>(_DestinationPoint, 0, 16);
                            return new IPEndPoint(new IPAddress(ipv6.ToArray()), BitConverter.ToInt32(_SourcePoint, 16));
                        default:
                            throw new InvalidOperationException("SourcePoint does not represent valid IPEndPoint.");
                    }
                }
            }
            set
            {
                // 4 or 16 bytes + 4 bytes
                _DestinationPoint = value.Address.GetAddressBytes().Concat(BitConverter.GetBytes(value.Port)).ToArray();
            }
        }

        public FlowKey Swap()
        {
            return new FlowKey()
            {
                Protocol = this.Protocol,
                SourcePoint = this.DestinationPoint,
                DestinationPoint = this.SourcePoint
            };
        }

        public bool Equals(FlowKey other)
        {
            if (other == null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this._Protocol != other._Protocol)
            {
                return false;
            }

            if (Utils.Equals(this._SourcePoint, other._SourcePoint))
            {
                return false;
            }

            return Utils.Equals(this._DestinationPoint, other._DestinationPoint);
        }

        public override int GetHashCode()
        {
            return (int)this._Protocol ^ Utils.GetHashCode(this._SourcePoint) ^ Utils.GetHashCode(this._DestinationPoint);
        }

        public class ReferenceComparer : IEqualityComparer<FlowKey>
        {
            public bool Equals(FlowKey x, FlowKey y)
            {
                return Object.ReferenceEquals(x, y);
            }

            public int GetHashCode(FlowKey obj)
            {
                return RuntimeHelpers.GetHashCode(obj);
            }
        }

        public class ValueComparer : IEqualityComparer<FlowKey>
        {
            public bool Equals(FlowKey x, FlowKey y)
            {
                return x?.Equals(y) ?? false;
            }

            public int GetHashCode(FlowKey obj)
            {
                return obj?.GetHashCode() ?? 0;
            }
        }
        public string IpFlowKeyString => $"{Protocol}!{SourceEndpoint}->{DestinationEndpoint}";
    }
}

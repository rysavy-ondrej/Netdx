using System;
using System.Linq;

namespace Netdx.Packets.Core
{

    /// <summary>
    /// Dns Packet decoder implementation.
    /// </summary>
    public partial class DnsPacket
    {
        /// <summary>
        /// Gets the domain name as a single string.
        /// </summary>
        public partial class DomainName
        {
            public string DomainNameString => String.Join(".", this.Name.Select(x => x.Name));

        }
    }
}
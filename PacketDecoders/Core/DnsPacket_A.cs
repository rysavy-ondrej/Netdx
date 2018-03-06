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
            string getLabelString(DnsPacket.Label label)
            {
                if (label.IsPointer)
                {
                    return label.Pointer.Contents.DomainNameString;
                }
                else
                {
                    return label.Name;
                }
            }

            public string DomainNameString
            {
                get
                {
                    
                    var result = String.Join(".", this.Labels.Select(getLabelString));
                    return result;
                }
            }

        }
    }
}
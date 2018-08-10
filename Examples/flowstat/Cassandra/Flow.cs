using Cassandra.Mapping;
using System;
using System.Net;

namespace Flowify.Cassandra
{

    /// <summary>
    /// Represents a single flow record.
    /// </summary>
    public class Flow
    {
        /// <summary>
        /// A unique identifier of the flow record.
        /// </summary>
        public string FlowId { get; set; }
        /// <summary>
        /// Type of transport (or internet) protocol of the flow.
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// The network source address of the flow.
        /// </summary>
        public string SourceAddress { get; set; }
        /// <summary>
        /// Source port (if any) of the flow.
        /// </summary>
        public int SourcePort { get; set; }
        /// <summary>
        /// The network destination address of the flow.
        /// </summary>
        public string DestinationAddress { get; set; }
        /// <summary>
        /// The destination port of the flow.
        /// </summary>
        /// <returns></returns>
        public int DestinationPort { get; set; }
        /// <summary>
        /// Unix time stamp of the start of flow.
        /// </summary>
        public Int64 FirstSeen { get; set; }
        /// <summary>
        /// The unix time stamp of the end of flow.
        /// </summary>
        /// <returns></returns>
        public Int64 LastSeen { get; set; }
        /// <summary>
        /// Number of packets carried by the flow.
        /// </summary>
        public Int32 Packets { get; set; }
        /// <summary>
        /// Total number of octets carried by the flow.
        /// </summary>
        /// <returns></returns>
        public Int64 Octets { get; set; }

        /// <summary>
        /// Creates an empty flow record.
        /// </summary>
        public Flow()
        {

        }
    }
}
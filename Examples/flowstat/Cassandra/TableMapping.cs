using Cassandra.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flowify.Cassandra
{
    class TableMapping: Mappings
    {
        public TableMapping()
        {
            For<Flow>().TableName("flows")
                .PartitionKey("protocol", "sourceAddress", "sourcePort", "destinationAddress", "destinationPort")
                .ClusteringKey(x => x.FlowId)
                .Column(f=>f.FlowId,c=>c.WithSecondaryIndex());
        }

        /// <summary>
        /// Registers the defined 
        /// </summary>
        /// <param name="config">Mapping configuration, usually MappingConfiguration.Global instance.</param>
        public static void Register(MappingConfiguration config)
        {
            config.Define<TableMapping>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Netdx.ConversationTracker {
    /// <summary>
    /// This is a generic class that provides basic functionality of tracking flows (flowify) in a sequence of input packets.
    /// </summary>
    /// <typeparam name="TPacket"></typeparam>
    /// <typeparam name="TFlowKey"></typeparam>
    /// <typeparam name="TFlowRecord"></typeparam>
    public class Tracker<TPacket, TFlowKey, TFlowRecord> {
        private IFlowTable<TFlowKey, TFlowRecord> m_flowTable;
        private IKeyProvider<TFlowKey, TPacket> m_keyProvider;
        private IRecordProvider<TPacket, TFlowRecord> m_recordProvider;
        public Tracker (IFlowTable<TFlowKey, TFlowRecord> table, IKeyProvider<TFlowKey, TPacket> keyProvider, IRecordProvider<TPacket, TFlowRecord> recordProvider) {
            m_flowTable = table;
            m_keyProvider = keyProvider;
            m_recordProvider = recordProvider;
        }

        /// <summary>
        /// Gets the flow record from flow table for the flow to which the given packet belongs.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns>The flow record or null if such record does not exist.</returns>
        public TFlowRecord PeekFlow (TPacket packet, out TFlowKey flowKey) {
            flowKey = m_keyProvider.GetKey (packet);
            return m_flowTable.Get (flowKey);
        }

        /// <summary>
        /// Updates existing flow or creates a new one for the given packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns>The flow record created for the given packet.</returns>
        public TFlowRecord UpdateFlow (TPacket packet, out TFlowKey flowKey) {
            flowKey = m_keyProvider.GetKey (packet);
            var record = m_flowTable.Get (flowKey);
            var updateRecord = m_recordProvider.GetRecord (packet);
            if (record == null) {
                m_flowTable.Put (flowKey, updateRecord);
                return updateRecord;
            } else {
                return m_flowTable.Merge (flowKey, updateRecord);
            }
        }
    }
}
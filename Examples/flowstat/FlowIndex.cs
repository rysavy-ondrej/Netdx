using System;
using System.Collections.Generic;
using System.Linq;
using Netdx.ConversationTracker;
namespace flowstat
{
    /// <summary>
    /// A flow index based on a hierarchy of bloom filters.
    /// </summary>
    public class FlowIndex
    {
        class FilterBlock
        {
            object m_syncObject;
            PcapPseudoheader m_header;
            BloomFilter<string> m_filter;
            /// <summary>
            /// Number of packet added to this block.
            /// </summary>
            int m_itemCount;
            /// <summary>
            /// The capacity of this block.
            /// </summary>
            int m_blockCapacity;
            public FilterBlock(int blockCapacity, int filterCapacity, Int32 linkType, long fileOffset)
            {
                m_header = new PcapPseudoheader
                {
                    LinkType = linkType,
                    FileOffset = fileOffset
                };
                m_filter = new BloomFilter<string>(filterCapacity);
                m_blockCapacity = blockCapacity;
                m_syncObject = new object();
            }
            public bool IsEmpty => m_itemCount == 0;
            public bool IsFull => !(m_itemCount < m_blockCapacity);

            public PcapPseudoheader Header => m_header;

            public bool Add(FlowKey flowkey)
            {
                lock (m_syncObject)
                {
                    if (m_itemCount < m_blockCapacity)
                    {
                        m_filter.Add(flowkey.ToString());
                        m_itemCount++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            public bool Contains(FlowKey flowkey)
            {
                return m_filter.Contains(flowkey.ToString());
            }
        }


        int m_linkType;
        int m_packetFlowRatio;
        int m_blockCapacity;
        /// <summary>
        /// A list of the lowest-level bloom filters.
        /// </summary>
        List<FilterBlock> m_filterArray;
        FilterBlock m_lastBlock;
        public FlowIndex(int blockCapacity, int packetFlowRatio, int linkType)
        {
            m_blockCapacity = blockCapacity;
            m_packetFlowRatio = packetFlowRatio;
            m_linkType = linkType;

            m_filterArray = new List<FilterBlock>();
            m_lastBlock = null;
        }

        public void Add(int packetNumber, long packetOffset, FlowKey flowkey)
        {

            if (m_lastBlock == null)
            {
                var filterCapacity = m_blockCapacity / m_packetFlowRatio;
                m_lastBlock = new FilterBlock(m_blockCapacity, filterCapacity,  m_linkType, packetOffset);
                m_filterArray.Add(m_lastBlock);
            }
            m_lastBlock.Add(flowkey);
            if (m_lastBlock.IsFull)
            {
                m_lastBlock = null;
            }
        }

        /// <summary>
        /// Returns a collection of blocks that contains packets of the specified flowKey.
        /// </summary>
        /// <param name="flowKey"></param>
        /// <returns>An enumerable of blocks containg the packets of the given flow key.</returns>
        public IEnumerable<PcapPseudoheader> Filter(FlowKey flowKey)
        {
            return m_filterArray.Where(x => x.Contains(flowKey)).Select(x => x.Header); 
        }
    }
}

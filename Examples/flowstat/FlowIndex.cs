using System;
using System.Collections.Generic;
using System.Linq;
using Netdx.ConversationTracker;
namespace Flowify
{
    /// <summary>
    /// A flow index based on bloom filters. Packets are organized into blocks. For each block 
    /// we compute a bloom filter using flow keys. Then it is possible to retrieve a list of blocks 
    /// with packets of the specified flow key. 
    /// </summary>
    public class FlowIndex
    {
        /// <summary>
        /// Represents a single block in the structure of bloom filters. 
        /// </summary>
        class FilterBlock
        {
            object m_syncObject;
            PcapPseudoheader m_header;
            BloomFilter<string> m_filter;
            /// <summary>
            /// Number of packets added to this block.
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
        /// <summary>
        /// The pointer to the last <see cref="FilterBlock"/>.
        /// </summary>
        FilterBlock m_lastBlock;
        /// <summary>
        /// Creates a new instance of <see cref="FlowIndex"/>. 
        /// </summary>
        /// <param name="blockCapacity">Desired block capacity. Block capacity influences the size of the Bloom filter in the following way: filterCapacity = blockCapacity / packetFlowRatio</param>
        /// <param name="packetFlowRatio">Expected ratio between packets and flows. For instance, if an average flow contains 10 packets that this ratio would be 10.</param>
        /// <param name="linkType">The type of link layer.</param>
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

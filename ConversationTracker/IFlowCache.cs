using System;
using System.Collections.Generic;
using System.Text;

namespace Netdx.ConversationTracker
{
    /// <summary>
    /// This interface specifies the minimum set of methods 
    /// required by the conversation tracker.
    /// </summary>
    public interface IFlowCache<TKey, TValue>
    {
        /// <summary>
        /// Inserts an item into the flow cache object with a cache key to reference its location.
        /// </summary>
        /// <param name="key">The cache key used to reference the item.</param>
        /// <param name="value">The object to be inserted into the cache.</param>
        void Insert(TKey key, TValue value);

        /// <summary>
        /// Retrieves the specified item from the flow cache object.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        TValue Get(TKey key);

        /// <summary>
        /// Removes the specified item from the flow cache object.
        /// </summary>
        /// <param name="key">A key identifier for the cache item to remove.</param>
        /// <returns>The item removed from the Cache. If the value in the key parameter is not found, returns null.</returns>
        TValue Remove(TKey key);
    }
}

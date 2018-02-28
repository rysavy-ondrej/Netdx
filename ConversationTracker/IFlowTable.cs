using System;
using System.Collections.Generic;
using System.Text;

namespace Netdx.ConversationTracker
{
    /// <summary>
    /// This interface specifies the minimum set of methods 
    /// required by the conversation tracker.
    /// </summary>
    public interface IFlowTable<TKey, TValue>
    {
        /// <summary>
        /// Inserts an item into the flow cache object with a cache key to reference its location.
        /// </summary>
        /// <param name="key">The cache key used to reference the item.</param>
        /// <param name="value">The object to be inserted into the cache.</param>
        void Put(TKey key, TValue value);

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
        TValue Delete(TKey key);


        /// <summary>
        /// Tests if the kye in the dabase exists.
        /// </summary>
        /// <param name="key">The kye of the record in the table.</param>
        /// <returns>true if the key exists in the table and false othewrise.</returns>
        bool Exists(TKey key);


        /// <summary>
        /// The merge operation implements read-modify-write sequence of operations. 
        /// It shold take a new value and merge it with the values already stored in the table.
        /// </summary>
        /// <param name="key">The key of the record.</param>
        /// <param name="value">The value to be merged with that stored in the table.</param>
        /// <returns>Returns the merged values. </returns>
        TValue Merge(TKey key, TValue value);


        /// <summary>
        /// Removes all items from the flow table.
        /// </summary>
        void FlushAll();
    }
}

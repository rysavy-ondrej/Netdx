using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Netdx.StreamLog
{
    /// <summary>
    /// Predicate is an expression followed by zero or more terms, in the form "pred(term1, term2, term3...)".
    /// </summary>
    /// <typeparam name="T">A type of expression. Usually, it will be <see cref="System.Tuple"/></typeparam>
    class Predicate<T>
    {        
        /// <summary>
        /// Gets or sets the name of the predicate.
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// A tuple that represents predicate terms. 
        /// </summary>
        T Terms { get; set; }
    }
}

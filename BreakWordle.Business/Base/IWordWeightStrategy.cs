using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business
{
    public interface IWordWeightStrategy
    {
        /// <summary>
        /// Gets the weight of a word by some means.
        /// </summary>
        /// <param name="word">The word to weigh.</param>
        /// <returns>A number.</returns>
        long GetWordWeight(string word);
    }
}

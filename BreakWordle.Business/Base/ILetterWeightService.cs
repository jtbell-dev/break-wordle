using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business
{
    public interface ILetterWeightService
    {
        /// <summary>
        /// Gets the weight of a letter based on the frequency of its occurrence.
        /// </summary>
        /// <param name="letter">The letter (usually alphabetical).</param>
        /// <returns>A number indicating the weight.</returns>
        long GetLetterWeight(char letter);

        /// <summary>
        /// Gets the weight of a letter based on the frequency of its occurrence at a specific index.
        /// </summary>
        /// <param name="letter">The letter (usually alphabetical).</param>
        /// <param name="position">The position to weigh.</param>
        /// <returns>A number indicating the weight.</returns>
        long GetLetterWeight(char letter, int position);
    }
}

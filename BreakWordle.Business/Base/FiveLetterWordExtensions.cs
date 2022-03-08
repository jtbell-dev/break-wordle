using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business
{
    public static class FiveLetterWordExtensions
    {
        /// <summary>
        /// Determines if the <see cref="FiveLetterWord"/> encapsulates a word with five letters.
        /// </summary>
        /// <param name="word">The five letter word object.</param>
        /// <returns>A boolean.</returns>
        public static bool IsValid(this FiveLetterWord word)
        {
            return !string.IsNullOrEmpty(word.Word) && FiveLetterWord.WordRegex.IsMatch(word.Word);
        }
    }
}

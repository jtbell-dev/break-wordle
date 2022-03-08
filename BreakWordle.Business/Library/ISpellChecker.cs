using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business.Library
{
    public interface ISpellChecker
    {
        /// <summary>
        /// Gets a value indicating whether a given string represents a valid English word.
        /// </summary>
        /// <param name="word">The string to test.</param>
        /// <returns>A boolean.</returns>
        bool IsEnglishWord(string word);
    }

    public class SpellChecker : ISpellChecker
    {
        private readonly IWordRetriever _wordRetriever;
        private readonly HashSet<string> _wordSet;

        public SpellChecker(IWordRetriever wordRetriever)
        {
            _wordRetriever = wordRetriever;
            _wordSet = new HashSet<string>(_wordRetriever.GetWords());
        }

        public bool IsEnglishWord(string word)
        {
            return _wordSet.Contains(word);
        }
    }
}

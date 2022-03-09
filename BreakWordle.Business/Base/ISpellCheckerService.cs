using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business
{
    public interface ISpellCheckerService
    {
        /// <summary>
        /// Gets a value indicating whether a given string represents a valid English word.
        /// </summary>
        /// <param name="word">The string to test.</param>
        /// <returns>A boolean.</returns>
        bool IsEnglishWord(string word);
    }

    public class SpellCheckerService : ISpellCheckerService
    {
        private readonly IWordRetrieverService _wordRetriever;
        private readonly HashSet<string> _wordSet;

        public SpellCheckerService(IWordRetrieverService wordRetriever)
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BreakWordle.Business
{
    public interface IWordRetrieverService
    {
        /// <summary>
        /// Gets a list of words from a source.
        /// </summary>
        /// <returns>A list of strings representing words.</returns>
        IEnumerable<string> GetWords();
    }

    /// <summary>
    /// Gets five letter words from list provided by https://github.com/dwyl/english-words
    /// </summary>
    public class FiveLetterWordRetriever : IWordRetrieverService
    {
        private readonly string _sourceFile = "words_alpha.txt";
        private IEnumerable<string> _wordSingleton;

        public FiveLetterWordRetriever()
        {

        }

        public IEnumerable<string> GetWords()
        {
            if (_wordSingleton is null)
            {
                var r = new Regex(@"^[a-zA-Z]{5}$");
                var sourcePath = Path.Combine(Environment.CurrentDirectory, Constants.WORD_FILES_FOLDERNAME, _sourceFile);
                _wordSingleton = 
                    File.ReadAllLines(sourcePath)
                    .Where(line => r.IsMatch(line.Trim()))
                    .Select(line => r.Match(line).Value);
            }

            return _wordSingleton;
        }
    }
}

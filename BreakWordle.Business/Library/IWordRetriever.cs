using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BreakWordle.Business.Library
{
    public interface IWordRetriever
    {
        /// <summary>
        /// Gets a list of words from a source.
        /// </summary>
        /// <returns>A list of strings representing words.</returns>
        IEnumerable<string> GetWords();
    }

    public class FiveLetterWordRetriever : IWordRetriever
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
                var sourcePath = Path.Combine(Environment.CurrentDirectory, Constants.WORD_FILES_FOLDERNAME, _sourceFile);
                _wordSingleton = 
                    File.ReadAllLines(sourcePath)
                    .Where(line => 
                        !string.IsNullOrEmpty(line) && 
                        line.Trim().Length == 5);
            }

            return _wordSingleton;
        }
    }
}

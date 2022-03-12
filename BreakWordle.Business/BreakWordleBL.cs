using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakWordle.Business
{
    public class BreakWordleBL
    {
        private readonly IWordWeightService _wordWeightService;
        private readonly IWordRetrieverService _wordRetrieverService;
        private readonly IEnumerable<string> _wordList;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreakWordleBL"/> class.
        /// </summary>
        /// <param name="wordWeightService"></param>
        /// <param name="wordList"></param>
        public BreakWordleBL(
            IWordWeightService wordWeightService,
            IWordRetrieverService wordRetrieverService)
        {
            _wordWeightService = wordWeightService;
            _wordRetrieverService = wordRetrieverService;
            _wordList = _wordRetrieverService.GetWords();
        }

        /// <summary>
        /// Gets the N best words to start with consecutively in Wordle/Quordle/Octordle
        /// </summary>
        /// <param name="n">The number of words you can use.</param>
        /// <returns>A list of words.</returns>
        public IEnumerable<string> GetBestStartingWords(int n)
        {
            List<string> results = new List<string>();
            Dictionary<char, string> lettersUsedAndWhichWordTheyBelongTo = new Dictionary<char, string>();
            var orderedWordList = _wordList.OrderByDescending(word => _wordWeightService.GetWordWeight(word)).ToList();
            var distinctLetterRequirement = 5;
            var alreadyUsedLetterThreshold = 0;
            while (results.Count < n)
            {
                Debug.WriteLine($"WORD REQUIREMENTS: " +
                        $"DISTINCT_LETTERS={distinctLetterRequirement}, " +
                        $"MATCHING_LETTERS={alreadyUsedLetterThreshold}");

                foreach (var word in orderedWordList)
                {
                    // only continue adding words while results has less than N values
                    if (results.Count >= n)
                        break;

                    // only use words that have 5 distinct characters
                    if (word.Distinct().Count() < distinctLetterRequirement)
                    {
                        Debug.WriteLine($"... DISTINCT LETTER THRESHOLD MET [{word.Distinct().Count()} < {distinctLetterRequirement}]. SKIPPING WORD: {word}");
                        continue;
                    }                        

                    // only use words with new letters
                    int lettersAlreadyUsedCount = 0;
                    bool letterAlreadyUsed = false;
                    foreach (var letter in word)
                    {
                        if (lettersUsedAndWhichWordTheyBelongTo.TryGetValue(letter, out var existingWord))
                        {
                            Debug.WriteLine($"... LETTER {letter} already used in {existingWord}...");
                            lettersAlreadyUsedCount++;
                            letterAlreadyUsed = lettersAlreadyUsedCount > alreadyUsedLetterThreshold;
                            if (letterAlreadyUsed)
                            {
                                Debug.WriteLine($"... THRESHOLD MET [{lettersAlreadyUsedCount}<{alreadyUsedLetterThreshold}]. SKIPPING WORD: {word}");
                                break;
                            }                                
                        }
                    }

                    if (letterAlreadyUsed)
                        continue;

                    // if code reaches this point, the word variable will be the most 
                    // heavily-weighted word with letters not previously used
                    Debug.WriteLine($"ADDING WORD: {word}");
                    results.Add(word);
                    foreach (var letter in word)
                    {
                        lettersUsedAndWhichWordTheyBelongTo.TryAdd(letter, word);
                    }
                }

                // remove used words from ordered set
                foreach (var word in results)
                {
                    if (orderedWordList.Contains(word))
                        orderedWordList.Remove(word);
                }

                // change thresholds if more words are needed
                if (results.Count < n)
                {
                    if (distinctLetterRequirement > 0)
                        distinctLetterRequirement--;
                    else
                    {
                        // reset already used letter threshold
                        distinctLetterRequirement = 5;

                        // decrement distinct letter requirement
                        alreadyUsedLetterThreshold++;
                    }
                }
            }

            Debug.WriteLine($"REMAINING WORDS:{Environment.NewLine}{string.Join(Environment.NewLine, orderedWordList)}");

            return results;
        }
    }
}

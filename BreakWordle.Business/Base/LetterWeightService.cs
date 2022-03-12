using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakWordle.Business
{
    /// <summary>
    /// Service which determines the weight of a letter based on how frequently it occurs
    /// in all words in a provided collection.
    /// </summary>
    public class LetterWeightService : ILetterWeightService
    {
        private Dictionary<int, Dictionary<char, long>> _letterWeightsByFrequencyInPosition;
        private Dictionary<char, long> _letterWeightsByFrequency;
        private readonly IEnumerable<string> _sourceWords;
        private readonly IWordRetrieverService _wordRetrieverService;
        public LetterWeightService(IWordRetrieverService wordRetrieverService)
        {
            _wordRetrieverService = wordRetrieverService ?? throw new ArgumentNullException(nameof(wordRetrieverService));
            _sourceWords = _wordRetrieverService.GetWords();
            _letterWeightsByFrequencyInPosition = new Dictionary<int, Dictionary<char, long>>();
            _letterWeightsByFrequency = new Dictionary<char, long>();
            int maxWordLength = _sourceWords.Max(x => x.Length);
            foreach (var i in Enumerable.Range(0, maxWordLength))
            {
                _letterWeightsByFrequencyInPosition.Add(i, new Dictionary<char, long>());
            }
                
            BuildLetterWeights();
        }

        /// <inheritdoc cref="ILetterWeightService.GetLetterWeight(char)"/>
        public long GetLetterWeight(char letter)
        {
            if (_letterWeightsByFrequency.TryGetValue(char.ToLower(letter), out var weight))
            {
                return weight;
            }

            if (_letterWeightsByFrequency.TryGetValue(char.ToUpper(letter), out weight))
            {
                return weight;
            }

            return 0;
        }

        /// <inheritdoc cref="ILetterWeightService.GetLetterWeight(char, int)"/>
        public long GetLetterWeight(char letter, int position)
        {
            if (_letterWeightsByFrequencyInPosition.TryGetValue(position, out var dict))
            {
                if (dict.TryGetValue(char.ToUpper(letter), out var weight))
                {
                    return weight;
                }

                if (dict.TryGetValue(char.ToLower(letter), out weight))
                {
                    return weight;
                }
            }

            return 0;
        }

        /// <summary>
        /// Build letter weight dictionary based on source words.
        /// </summary>
        private void BuildLetterWeights()
        {
            foreach (var word in _sourceWords)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    var letter = word[i];

                    // update overall frequency
                    if (!_letterWeightsByFrequency.TryGetValue(letter, out _))
                    {
                        _letterWeightsByFrequency.Add(letter, 0);
                    }

                    _letterWeightsByFrequency[letter]++;

                    // update frequency by position
                    var positionDict = _letterWeightsByFrequencyInPosition[i];
                    if (!positionDict.TryGetValue(letter, out _))
                    {
                        positionDict.Add(letter, 0);
                    }

                    positionDict[letter]++;
                    _letterWeightsByFrequencyInPosition[i] = positionDict;
                }
            }
        }
    }
}

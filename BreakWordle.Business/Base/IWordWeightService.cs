using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business
{
    public interface IWordWeightService
    {
        /// <summary>
        /// Determines the weight of a word based on some logic.        
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>A number indicating the weight of a word.</returns>
        long GetWordWeight(string word);
    }

    public class WordWeightService : IWordWeightService
    {        
        private Dictionary<string, long> _wordWeights;
        private readonly IEnumerable<string> _sourceWords;
        private readonly ILetterWeightService _letterWeightService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordWeightService"/> class.
        /// </summary>
        /// <remarks>
        /// The <see cref="ILetterWeightService"/> must pull from the same list of words as this object.
        /// </remarks>
        /// <param name="letterWeightService">The letter weight service.</param>
        /// <param name="sourceWords">The list of words.</param>
        public WordWeightService(ILetterWeightService letterWeightService, IEnumerable<string> sourceWords)
        {
            _wordWeights = new Dictionary<string, long>();
            _sourceWords = sourceWords ?? throw new ArgumentNullException(nameof(sourceWords));
            _letterWeightService = letterWeightService ?? throw new ArgumentNullException(nameof(letterWeightService));
            BuildWordWeights();
        }

        /// <inheritdoc cref="IWordWeightService.GetWordWeight(string)"/>
        public long GetWordWeight(string word)
        {
            if (_wordWeights.TryGetValue(word.ToUpper(), out var weight))
            {
                return weight;
            }

            if (_wordWeights.TryGetValue(word.ToLower(), out weight))
            {
                return weight;
            }

            return 0;
        }

        /// <summary>
        /// Build word weight dictionary based on source words.
        /// </summary>
        private void BuildWordWeights()
        {
            foreach (var word in _sourceWords)
            {
                if (!_wordWeights.TryGetValue(word, out var weight))
                {
                    _wordWeights.Add(word, 0);
                }

                foreach (var letter in word)
                {
                    _wordWeights[word] += _letterWeightService.GetLetterWeight(letter);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business.Strategies
{
    public class FrequencyWeightStrategy : IWordWeightStrategy
    {
        private Dictionary<string, long> _wordWeights;
        private readonly IEnumerable<string> _sourceWords;
        private readonly IWordRetrieverService _wordRetrieverService;
        private readonly ILetterWeightService _letterWeightService;
        public FrequencyWeightStrategy(
            IWordRetrieverService wordRetrieverService,
            ILetterWeightService letterWeightService)
        {
            _wordRetrieverService = wordRetrieverService ?? throw new ArgumentNullException(nameof(wordRetrieverService));
            _letterWeightService = letterWeightService ?? throw new ArgumentNullException(nameof(letterWeightService));
            _sourceWords = _wordRetrieverService.GetWords();
            _wordWeights = new Dictionary<string, long>();
            BuildWordWeights();
        }

        /// <inheritdoc cref="IWordWeightStrategy.GetWordWeight(string)"/>
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

        private void BuildWordWeights()
        {
            foreach (var word in _sourceWords)
            {
                _wordWeights.Add(word, 0);

                foreach (var letter in word)
                {
                    _wordWeights[word] += _letterWeightService.GetLetterWeight(letter);
                }
            }
        }
    }
}

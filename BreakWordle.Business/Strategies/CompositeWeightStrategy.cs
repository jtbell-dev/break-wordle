using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business.Strategies
{
    public class CompositeWeightStrategy : IWordWeightStrategy
    {
        private Dictionary<string, long> _averageWordWeights;
        private readonly IEnumerable<string> _sourceWords;
        private readonly IWordRetrieverService _wordRetrieverService;
        private readonly ICollection<IWordWeightStrategy> _strategies;
        public CompositeWeightStrategy(
            IWordRetrieverService wordRetrieverService,
            ICollection<IWordWeightStrategy> strategies)
        {
            _wordRetrieverService = wordRetrieverService ?? throw new ArgumentNullException(nameof(wordRetrieverService));
            _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
            _sourceWords = _wordRetrieverService.GetWords();
            _averageWordWeights = new Dictionary<string, long>();
            BuildWordWeights();
        }

        /// <inheritdoc cref="IWordWeightStrategy.GetWordWeight(string)"/>
        public long GetWordWeight(string word)
        {
            if (_averageWordWeights.TryGetValue(word.ToUpper(), out var weight))
            {
                return weight;
            }

            if (_averageWordWeights.TryGetValue(word.ToLower(), out weight))
            {
                return weight;
            }

            return 0;
        }

        private void BuildWordWeights()
        {
            foreach (var word in _sourceWords)
            {
                double sum = 0;
                double count = 0;
                foreach (var strategy in _strategies)
                {
                    var weight = strategy.GetWordWeight(word);
                    count += 1;
                    sum += weight;
                }

                if (count > 0)
                {
                    _averageWordWeights.Add(word, (long)Math.Ceiling(sum / count));
                }
                else
                {
                    _averageWordWeights.Add(word, 0);
                }
            }
        }
    }
}

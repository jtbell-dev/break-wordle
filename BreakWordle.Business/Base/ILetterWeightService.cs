using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business
{
    public interface ILetterWeightService
    {
        /// <summary>
        /// Gets the weight of a letter.
        /// </summary>
        /// <param name="letter">The letter (usually alphabetical).</param>
        /// <returns>A number indicating the weight.</returns>
        long GetLetterWeight(char letter);
    }

    public class LetterWeightService : ILetterWeightService
    {
        private Dictionary<char, long> _letterWeights;
        private readonly IEnumerable<string> _sourceWords;
        public LetterWeightService(IEnumerable<string> sourceWords)
        {
            _letterWeights = new Dictionary<char, long>();
            _sourceWords = sourceWords ?? throw new ArgumentNullException(nameof(sourceWords));
            BuildLetterWeights();
        }

        /// <inheritdoc cref="ILetterWeightService.GetLetterWeight(char)"/>
        public long GetLetterWeight(char letter)
        {
            if (_letterWeights.TryGetValue(char.ToLower(letter), out var weight))
            {
                return weight;
            }

            if (_letterWeights.TryGetValue(char.ToUpper(letter), out weight))
            {
                return weight;
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
                foreach (var letter in word)
                {
                    if (!_letterWeights.TryGetValue(letter, out var weight))
                    {
                        _letterWeights.Add(letter, 0);
                    }

                    _letterWeights[letter]++;
                }
            }
        }
    }
}

using BreakWordle.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Tests
{
    [TestClass]
    public class WordWeightTests
    {
        [TestMethod]
        public void TestWordWeightService()
        {
            var words = new string[]
            {
                "adieu",
                "snort",
                "oater"
            };

            var expectedWeights = new Dictionary<string, long>()
            {
                // LETTER WEIGHTS:
                //{ 'A', 2 },
                //{ 'D', 1 },
                //{ 'I', 1 },
                //{ 'E', 2 },
                //{ 'U', 1 },
                //{ 'S', 1 },
                //{ 'N', 1 },
                //{ 'O', 2 },
                //{ 'R', 2 },
                //{ 'T', 2 }

                { "adieu", 7 },
                { "snort", 8 },
                { "oater", 10 }
            };

            var letterWeightService = new LetterWeightService(words);
            var service = new WordWeightService(letterWeightService, words);
            foreach (var word in expectedWeights.Keys)
            {
                var expectedWeight = expectedWeights[word];
                var actualWeight = service.GetWordWeight(word);
                Assert.AreEqual(expectedWeight, actualWeight, "Values should match.");
            }
        }
    }
}

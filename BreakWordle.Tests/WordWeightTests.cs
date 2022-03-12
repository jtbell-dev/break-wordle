using BreakWordle.Business;
using BreakWordle.Business.Strategies;
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

            var wordRetrieverService = new TestWordRetrieverService();
            var letterWeightService = new LetterWeightService(wordRetrieverService);
            var wordWeightStrategy = new FrequencyWeightStrategy(wordRetrieverService, letterWeightService);
            var service = new WordWeightService(wordRetrieverService, wordWeightStrategy);
            foreach (var word in expectedWeights.Keys)
            {
                var expectedWeight = expectedWeights[word];
                var actualWeight = service.GetWordWeight(word);
                Assert.AreEqual(expectedWeight, actualWeight, "Values should match.");
            }
        }
    }
}

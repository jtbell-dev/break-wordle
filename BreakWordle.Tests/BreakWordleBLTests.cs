using BreakWordle.Business;
using BreakWordle.Business.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BreakWordle.Tests
{
    [TestClass]
    public class BreakWordleBLTests
    {
        [TestMethod]
        public void TestBreakWordleBL()
        {
            var wordRetrieverService = new FiveLetterWordRetrieverService();
            var spellCheckerService = new SpellCheckerService(wordRetrieverService);
            var letterWeightService = new LetterWeightService(wordRetrieverService);
            var wordWeightStrategy = new CompositeWeightStrategy(wordRetrieverService, new List<IWordWeightStrategy>()
            {
                new FrequencyAndPositionWeightStrategy(wordRetrieverService, letterWeightService),
                new FrequencyWeightStrategy(wordRetrieverService, letterWeightService),
                new PositionWeightStrategy(wordRetrieverService, letterWeightService)
            });
            var wordWeightService = new WordWeightService(wordRetrieverService, wordWeightStrategy);
            var bl = new BreakWordleBL(wordWeightService, wordRetrieverService);
            var results = bl.GetBestStartingWords(5);
            Debug.WriteLine($"WORDS: {string.Join(", ", results)}");
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.AreEqual(5, results.Count());

            // FIRST RUN:
            // WORDS: arose, unlit, pygmy, chawk, fazed

            // SECOND RUN:
            // WORDS: tares, colin, dumpy, hewgh, jakob
        }
    }
}

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
        public void TestBreakWordleBL_FrequencyAndPositionWeightStrategy()
        {
            var wordRetrieverService = new FiveLetterWordRetrieverService();
            var spellCheckerService = new SpellCheckerService(wordRetrieverService);
            var letterWeightService = new LetterWeightService(wordRetrieverService);
            var wordWeightStrategy = new CompositeWeightStrategy(wordRetrieverService, new List<IWordWeightStrategy>()
            {
                new FrequencyAndPositionWeightStrategy(wordRetrieverService, letterWeightService)
            });
            var wordWeightService = new WordWeightService(wordRetrieverService, wordWeightStrategy);
            var bl = new BreakWordleBL(wordWeightService, wordRetrieverService);
            var results = bl.GetBestStartingWords(5);
            Debug.WriteLine($"WORDS: {string.Join(", ", results)}");
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.AreEqual(5, results.Count());

            // WORDS: tares, colin, dumpy, hewgh, jakob
        }

        [TestMethod]
        public void TestBreakWordleBL_FrequencyWeightStrategy_PositionWeightStrategy()
        {
            var wordRetrieverService = new FiveLetterWordRetrieverService();
            var spellCheckerService = new SpellCheckerService(wordRetrieverService);
            var letterWeightService = new LetterWeightService(wordRetrieverService);
            var wordWeightStrategy = new CompositeWeightStrategy(wordRetrieverService, new List<IWordWeightStrategy>()
            {
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

            // WORDS: tares, colin, dumpy, hewgh, jakob
        }

        [TestMethod]
        public void TestBreakWordleBL_FrequencyWeightStrategy()
        {
            var wordRetrieverService = new FiveLetterWordRetrieverService();
            var spellCheckerService = new SpellCheckerService(wordRetrieverService);
            var letterWeightService = new LetterWeightService(wordRetrieverService);
            var wordWeightStrategy = new CompositeWeightStrategy(wordRetrieverService, new List<IWordWeightStrategy>()
            {
                new FrequencyWeightStrategy(wordRetrieverService, letterWeightService)
            });
            var wordWeightService = new WordWeightService(wordRetrieverService, wordWeightStrategy);
            var bl = new BreakWordleBL(wordWeightService, wordRetrieverService);
            var results = bl.GetBestStartingWords(5);
            Debug.WriteLine($"WORDS: {string.Join(", ", results)}");
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.AreEqual(5, results.Count());

            // WORDS: arose, unlit, pygmy, chawk, fazed
        }

        [TestMethod]
        public void TestBreakWordleBL_PositionWeightStrategy()
        {
            var wordRetrieverService = new FiveLetterWordRetrieverService();
            var spellCheckerService = new SpellCheckerService(wordRetrieverService);
            var letterWeightService = new LetterWeightService(wordRetrieverService);
            var wordWeightStrategy = new CompositeWeightStrategy(wordRetrieverService, new List<IWordWeightStrategy>()
            {
                new PositionWeightStrategy(wordRetrieverService, letterWeightService)
            });
            var wordWeightService = new WordWeightService(wordRetrieverService, wordWeightStrategy);
            var bl = new BreakWordleBL(wordWeightService, wordRetrieverService);
            var results = bl.GetBestStartingWords(5);
            Debug.WriteLine($"WORDS: {string.Join(", ", results)}");
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.AreEqual(5, results.Count());

            // WORDS: cares, boily, punkt, zhmud, jaggs
        }
    }
}

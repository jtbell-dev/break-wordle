using BreakWordle.Business;
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
            var words = wordRetrieverService.GetWords();
            var letterWeightService = new LetterWeightService(words);
            var wordWeightService = new WordWeightService(letterWeightService, words);
            var bl = new BreakWordleBL(wordWeightService, words);
            var results = bl.GetBestStartingWords(5);
            Debug.WriteLine($"WORDS: {string.Join(", ", results)}");
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.AreEqual(5, results.Count());

            // WORDS: arose, unlit, pygmy, chawk, fazed
        }
    }
}

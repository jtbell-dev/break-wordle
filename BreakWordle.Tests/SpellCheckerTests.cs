using BreakWordle.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Tests
{
    [TestClass]
    public class SpellCheckerTests
    {
        [TestMethod]
        public void TestSpellChecking_LowerCase()
        {
            var words = new string[]
            {
                "adieu",
                "snort",
                "oater"
            };

            var wordRetriever = new FiveLetterWordRetriever();
            var service = new SpellCheckerService(wordRetriever);
            foreach (var word in words)
            {
                var isWord = service.IsEnglishWord(word);
                Assert.IsTrue(isWord);
            }
        }
    }
}

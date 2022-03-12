using BreakWordle.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Tests
{
    [TestClass]
    public class LetterWeightServiceTests
    {
        [TestMethod]
        public void TestLetterWeightService()
        {
            var expectedWeights = new Dictionary<char, long>()
            {
                { 'A', 2 },
                { 'D', 1 },
                { 'I', 1 },
                { 'E', 2 },
                { 'U', 1 },
                { 'S', 1 },
                { 'N', 1 },
                { 'O', 2 },
                { 'R', 2 },
                { 'T', 2 },
                { 't', 2 },
                { 'Z', 0 },
                { 'z', 0 }
            };

            var wordRetrieverService = new TestWordRetrieverService();
            var service = new LetterWeightService(wordRetrieverService);
            foreach (var letter in expectedWeights.Keys)
            {
                var expectedWeight = expectedWeights[letter];
                var actualWeight = service.GetLetterWeight(letter);
                Assert.AreEqual(expectedWeight, actualWeight, "Values should match.");
            }
        }
    }
}

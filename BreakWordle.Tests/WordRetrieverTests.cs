using Microsoft.VisualStudio.TestTools.UnitTesting;
using BreakWordle.Business;
using System.Linq;

namespace BreakWordle.Tests
{
    [TestClass]
    public class WordRetrieverTests
    {
        /// <summary>
        /// Verify functionality of <see cref="FiveLetterWordRetriever"/> service.
        /// </summary>
        [TestMethod]
        public void TestLibraryImport()
        {
            var wordRetriever = new FiveLetterWordRetriever();
            var lib = wordRetriever.GetWords();
            Assert.IsNotNull(lib);
            Assert.IsTrue(lib.Any());
        }
    }
}

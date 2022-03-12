using BreakWordle.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Tests
{
    public class TestWordRetrieverService : IWordRetrieverService
    {
        public IEnumerable<string> GetWords()
        {
            var words = new string[]
            {
                "adieu",
                "snort",
                "oater"
            };

            return words;
        }
    }
}

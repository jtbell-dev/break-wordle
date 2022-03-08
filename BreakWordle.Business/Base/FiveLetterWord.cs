using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BreakWordle.Business
{
    public class FiveLetterWord
    {
        internal static Regex WordRegex = new Regex(@"^[A-Za-z]{5}$");

        public FiveLetterWord(string word)
        {
            var temp = word?.ToUpper() ?? string.Empty;
            var isMatch = WordRegex.IsMatch(temp);
            if (isMatch)
                Word = word.ToUpper();
            else
                throw new ArgumentException($"{nameof(word)} must consist of exactly five alphabetical characters.");
        }

        public string Word { get; }
    }
}

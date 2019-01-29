using System.Collections.Generic;

namespace Sber.App.Helpers
{
    public static class LinksHelper
    {
        private static readonly char[] Digits = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K' };

        public static string GetShortValue(int counterSequenceValue)
        {
            var result = new List<char>();
            var mod = counterSequenceValue % Digits.Length;
            var div = counterSequenceValue / Digits.Length;
            result.Add(Digits[mod]);

            while (div != 0)
            {
                mod = div % Digits.Length;
                div = div / Digits.Length;
                result.Add(Digits[mod]);
            }


            return string.Concat(result);
        }
    }
}

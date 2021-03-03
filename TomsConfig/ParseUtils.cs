using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TomsConfig {
    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    internal static class ParseUtils {
        private static readonly char[] _quotes = {'"', '\''};
        private static readonly char[] _separators = {','};
        private static readonly char[] _arrayOpen = {'[', '(', '{'};
        private static readonly char[] _arrayClose = {']', ')', '}'};

        internal static string Parse(string s) {
            if (s[0] != s[^1] ||
                !_quotes.Contains(s[0]) ||
                !_quotes.Contains(s[^1]))
                throw new Exception("Strings start or end characters do not match");

            return s.Substring(1, s.Length - 2);
        }

        // ReSharper disable once CognitiveComplexity
        internal static string[] ParseStringArray(string s) {
            if (!_arrayOpen.Contains(s[0]) ||
                !_arrayClose.Contains(s[^1]))
                throw new Exception("Arrays start or end character is not correct");

            var items = new List<string>();
            var current = string.Empty;
            var quote = '\0';
            for (var i = 1; i < s.Length - 2; i++)
                if (quote == '\0' && _separators.Contains(s[i])) {
                    items.Add(Parse(current));
                    current = string.Empty;
                }
                else if (quote == '\0' && _quotes.Contains(s[i])) {
                    current += s[i];
                    quote = s[i];
                }
                else if (quote != '\0' && quote == s[i]) {
                    current += s[i];
                    quote = '\0';
                }
                else if (quote != '\0') {
                    current += s[i];
                }
                else if (char.IsLetterOrDigit(s[i])) {
                    throw new Exception("Error while parsing string array");
                }

            items.Add(Parse(current));
            return items.ToArray();
        }


        private static T[] ParseParsable<T>(string s, Func<string, T> func) {
            if (!_arrayOpen.Contains(s[0]) ||
                !_arrayClose.Contains(s[^1]))
                throw new Exception("Arrays start or end character is not correct");

            var items = new List<T>();
            var current = string.Empty;
            for (var i = 1; i < s.Length - 2; i++)
                if (_separators.Contains(s[i])) {
                    items.Add(func(current.Trim()));
                    current = string.Empty;
                }
                else {
                    current += s[i];
                }

            items.Add(func(current.Trim()));
            return items.ToArray();
        }

        internal static int[] ParseInt(string s) {
            return ParseParsable(s, int.Parse);
        }

        internal static double[] ParseDouble(string s) {
            return ParseParsable(s, double.Parse);
        }

        internal static float[] ParseFloat(string s) {
            return ParseParsable(s, float.Parse);
        }

        internal static long[] ParseLong(string s) {
            return ParseParsable(s, long.Parse);
        }

        internal static bool[] ParseBool(string s) {
            return ParseParsable(s, bool.Parse);
        }
    }
}
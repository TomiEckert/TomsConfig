using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TomsConfig {
    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public static class ParseUtils {
        private static readonly char[] _quotes = {'"', '\''};
        private static readonly char[] _arrayOpen = {'[', '(', '{'};
        private static readonly char[] _arrayClose = {']', ')', '}'};

        internal static string Parse(string s) {
            if (s[0] != s[^1] ||
                !_quotes.Contains(s[0]) ||
                !_quotes.Contains(s[^1]))
                throw new Exception("Strings start or end characters do not match");

            return s.Substring(1, s.Length - 2);
        }

        internal static string[] ParseStringArray(string s) {
            if (!_arrayOpen.Contains(s[0]) ||
                !_arrayClose.Contains(s[^1]))
                throw new Exception("Arrays start or end character is not correct");
            var pure = s.Substring(1, s.Length - 2);

            var temp = pure.Split(_quotes[0])
                           .Select((x, i) => i % 2 == 0 ? x.Trim().Split(_quotes[1]) : new[] {x.Trim()}).First()
                           .Select((x, i) => i % 2 == 0 ? x.Trim().Split(',') : new[] {x.Trim()})
                           .Select(x => x[0])
                           .Where(x => x.Trim() != string.Empty);
            return temp.ToArray();
        }

        private static T[] ParseParsable<T>(string s, Func<string, T> func) {
            if (!_arrayOpen.Contains(s[0]) ||
                !_arrayClose.Contains(s[^1]))
                throw new Exception("Arrays start or end character is not correct");

            var parts = s.Substring(1, s.Length - 2).Split(',');
            var result = parts.Select(x => func(x.Trim()));
            return result.ToArray();
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
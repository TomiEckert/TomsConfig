using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TomsConfig {
    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public class Parser : IParser {
        private readonly char[] _quotes = {'"', '\''};
        private readonly char[] _arrayOpen = {'[', '(', '{'};
        private readonly char[] _arrayClose = {']', ')', '}'};
        private string _value;

        void IParser.SetValue(string value) {
            _value = value;
        }

        int IParser.GetInt() {
            return int.Parse(_value);
        }

        string IParser.GetString() {
            return ParseString(_value);
        }

        double IParser.GetDouble() {
            return double.Parse(_value);
        }

        bool IParser.GetBool() {
            return bool.Parse(_value);
        }

        int[] IParser.GetInts() {
            return ParseParsable(_value, int.Parse);
        }

        double[] IParser.GetDoubles() {
            return ParseParsable(_value, double.Parse);
        }

        bool[] IParser.GetBools() {
            return ParseParsable(_value, bool.Parse);
        }

        string[] IParser.GetStrings() {
            if (!_arrayOpen.Contains(_value[0]) ||
                !_arrayClose.Contains(_value[^1]))
                throw new Exception("Arrays start or end character is not correct");
            var pure = _value.Substring(1, _value.Length - 2);

            var temp = pure.Split(_quotes[0])
                           .Select((x, i) => i % 2 == 0 ? x.Trim().Split(_quotes[1]) : new[] {x.Trim()}).First()
                           .Select((x, i) => i % 2 == 0 ? x.Trim().Split(',') : new[] {x.Trim()})
                           .Select(x => x[0])
                           .Where(x => x.Trim() != string.Empty);
            return temp.ToArray();
        }
        
        private string ParseString(string s) {
            if (s[0] != s[^1] ||
                !_quotes.Contains(s[0]) ||
                !_quotes.Contains(s[^1]))
                throw new Exception("Strings start or end characters do not match");

            return s.Substring(1, s.Length - 2);
        }

        private T[] ParseParsable<T>(string s, Func<string, T> func) {
            if (!_arrayOpen.Contains(s[0]) ||
                !_arrayClose.Contains(s[^1]))
                throw new Exception("Arrays start or end character is not correct");

            var parts = s.Substring(1, s.Length - 2).Split(',');
            var result = parts.Select(x => func(x.Trim()));
            return result.ToArray();
        }
    }
}
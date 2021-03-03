#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;

namespace TomsConfig {
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ConfigItem {
        internal ConfigItem(string value) {
            _value = value;
        }

        private readonly string _value;

        public int GetInteger() {
            return int.Parse(_value);
        }

        public string GetString() {
            return ParseUtils.Parse(_value);
        }

        public double GetDouble() {
            return double.Parse(_value);
        }

        public float GetFloat() {
            return float.Parse(_value);
        }

        public bool GetBool() {
            return bool.Parse(_value);
        }

        public decimal GetDecimal() {
            return decimal.Parse(_value);
        }

        public long GetLong() {
            return long.Parse(_value);
        }

        public bool TryGetInteger(out int i) {
            return int.TryParse(_value, out i);
        }

        public bool TryGetDouble(out double d) {
            return double.TryParse(_value, out d);
        }

        public bool TryGetFloat(out float f) {
            return float.TryParse(_value, out f);
        }

        public bool TryGetBool(out bool b) {
            return bool.TryParse(_value, out b);
        }

        public bool TryGetDecimal(out decimal d) {
            return decimal.TryParse(_value, out d);
        }

        public bool TryGetLong(out long l) {
            return long.TryParse(_value, out l);
        }

        public string[] GetStringArray() {
            return ParseUtils.ParseStringArray(_value);
        }

        public int[] GetIntegerArray() {
            return ParseUtils.ParseInt(_value);
        }

        public float[] GetFloatArray() {
            return ParseUtils.ParseFloat(_value);
        }

        public double[] GetDoubleArray() {
            return ParseUtils.ParseDouble(_value);
        }

        public long[] GetLongArray() {
            return ParseUtils.ParseLong(_value);
        }

        public bool[] GetBoolArray() {
            return ParseUtils.ParseBool(_value);
        }

        public bool Equals(ConfigItem item) {
            return _value == item._value;
        }

        public override bool Equals(object? obj) {
            return obj is ConfigItem item && Equals(item);
        }

        public override int GetHashCode() {
            return HashCode.Combine(_value);
        }

        public static bool operator ==(ConfigItem left, ConfigItem right) {
            return left.Equals(right);
        }

        public static bool operator !=(ConfigItem left, ConfigItem right) {
            return !left.Equals(right);
        }
    }
}
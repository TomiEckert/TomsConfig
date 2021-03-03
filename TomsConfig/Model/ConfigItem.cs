#nullable enable
using System;

namespace TomsConfig.Model {
    public class ConfigItem : IConfigItem{
        internal ConfigItem(string value) {
            _value = value;
        }

        private readonly string _value;

        int IConfigItem.GetInt() {
            return int.Parse(_value);
        }

        string IConfigItem.GetString() {
            return ParseUtils.Parse(_value);
        }

        double IConfigItem.GetDouble() {
            return double.Parse(_value);
        }

        bool IConfigItem.GetBool() {
            return bool.Parse(_value);
        }

        string[] IConfigItem.GetStrings() {
            return ParseUtils.ParseStringArray(_value);
        }

        int[] IConfigItem.GetInts() {
            return ParseUtils.ParseInt(_value);
        }

        double[] IConfigItem.GetDoubles() {
            return ParseUtils.ParseDouble(_value);
        }

        bool[] IConfigItem.GetBools() {
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
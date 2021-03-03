#nullable enable
namespace TomsConfig.Model {
    public class ConfigItem : IConfigItem {
        internal ConfigItem(string value, IParser parser) {
            _value = value;
            _parser = parser;
            _parser.SetValue(value);
        }

        private readonly IParser _parser;

        private readonly string _value;

        public IParser GetParser() {
            return _parser;
        }

        object IConfigItem.GetObject() {
            return _value;
        }

        public bool Equals(IConfigItem item) {
            return ((IConfigItem) this).GetObject().ToString() == item.GetObject().ToString();
        }
    }
}
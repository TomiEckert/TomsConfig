namespace TomsConfig.Model {
    public interface IConfigItem {
        public bool Equals(IConfigItem item);
        public IParser GetParser();
        public object GetObject();
    }
}
namespace TomsConfig.Model {
    public interface IConfigFactory {
        public IConfig CreateConfig();
        public IConfigBlock CreateBlock();
        public IConfigItem CreateItem(string value);
    }
}
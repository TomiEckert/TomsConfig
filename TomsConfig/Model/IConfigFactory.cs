namespace TomsConfig.Model {
    internal interface IConfigFactory {
        internal IConfig CreateConfig();
        internal IConfigBlock CreateBlock();
        internal IConfigItem CreateItem(string value);
    }
}
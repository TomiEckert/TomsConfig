namespace TomsConfig.Model {
    public class ConfigFactory : IConfigFactory {
        IConfig IConfigFactory.CreateConfig() {
            return new Config();
        }

        IConfigBlock IConfigFactory.CreateBlock() {
            return new ConfigBlock();
        }

        IConfigItem IConfigFactory.CreateItem(string value) {
            return new ConfigItem(value);
        }
    }
}
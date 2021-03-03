using System.Collections.Generic;

namespace TomsConfig.Model {
    public interface IConfig {
        public IConfigBlock this[string name] { get; }
        public void Add(string key, IConfigBlock block);
        public IEnumerable<string> GetBlocks();
    }
}
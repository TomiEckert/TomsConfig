using System.Collections.Generic;

namespace TomsConfig.Model {
    public interface IConfigBlock {
        public IConfigItem this[string name] { get; }
        public void Add(string key, IConfigItem item);
        public IEnumerable<string> GetItems();
    }
}
using System.Collections.Generic;

namespace TomsConfig {
    public interface IConfigBlock {
        
        public IConfigItem this[string name] { get; }
        internal void Add(IConfigItem item);
        public IEnumerable<string> GetItems();
    }
}
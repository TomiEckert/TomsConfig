using System.Collections.Generic;

namespace TomsConfig {
    public interface IConfig {
        public IConfigBlock this[string name] { get; }
        internal void Add(IConfigBlock block);
        public IEnumerable<string> GetBlocks();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace TomsConfig.Model {
    public class Config : IConfig {
        internal Config() {
            _blocks = new Dictionary<string, IConfigBlock>();
        }

        private readonly Dictionary<string, IConfigBlock> _blocks;

        IConfigBlock IConfig.this[string name] {
            get {
                try {
                    var block = _blocks.Single(x => x.Key == name);
                    return block.Value;
                }
                catch (InvalidOperationException) {
                    throw new InvalidOperationException("The provided " + nameof(name) + " '" + name +
                                                        "' was not found in blocks.");
                }
                catch (NullReferenceException) {
                    throw new InvalidOperationException("The provided " + nameof(name) +
                                                        " was null, please don't do that.");
                }
            }
        }

        void IConfig.Add(string key, IConfigBlock block) {
            if (_blocks.ContainsKey(key))
                _blocks[key] = block;
            else
                _blocks.Add(key, block);
        }

        IEnumerable<string> IConfig.GetBlocks() {
            return _blocks.Select(x => x.Key);
        }
    }
}
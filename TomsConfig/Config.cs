using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TomsConfig {
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Config {
        internal Config() {
            _blocks = new Dictionary<string, ConfigBlock>();
        }

        private readonly Dictionary<string, ConfigBlock> _blocks;

        public ConfigBlock this[string name] {
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

        internal void Add(string key, ConfigBlock block) {
            if (_blocks.ContainsKey(key))
                _blocks[key] = block;
            else
                _blocks.Add(key, block);
        }

        public IEnumerable<string> GetBlocks() {
            return _blocks.Select(x => x.Key);
        }
    }
}
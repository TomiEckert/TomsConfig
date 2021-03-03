using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TomsConfig {
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ConfigBlock {
        internal ConfigBlock() {
            _items = new Dictionary<string, ConfigItem>();
        }

        private readonly Dictionary<string, ConfigItem> _items;

        public ConfigItem this[string name] {
            get {
                try {
                    var item = _items.Single(x => x.Key == name);
                    return item.Value;
                }
                catch (InvalidOperationException) {
                    throw new InvalidOperationException("The provided " + nameof(name) + " '" + name +
                                                        "' was not found in items.");
                }
                catch (NullReferenceException) {
                    throw new InvalidOperationException("The provided " + nameof(name) +
                                                        " was null, please don't do that.");
                }
            }
        }

        internal void Add(string key, ConfigItem item) {
            if (_items.ContainsKey(key))
                _items[key] = item;
            else
                _items.Add(key, item);
        }

        public IEnumerable<string> GetItems() {
            return _items.Select(x => x.Key);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TomsConfig.Model {
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ConfigBlock : IConfigBlock {
        internal ConfigBlock() {
            _items = new Dictionary<string, IConfigItem>();
        }

        private readonly Dictionary<string, IConfigItem> _items;

        IConfigItem IConfigBlock.this[string name] {
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

        void IConfigBlock.Add(string key, IConfigItem item) {
            if (_items.ContainsKey(key))
                _items[key] = item;
            else
                _items.Add(key, item);
        }

        IEnumerable<string> IConfigBlock.GetItems() {
            return _items.Select(x => x.Key);
        }
    }
}
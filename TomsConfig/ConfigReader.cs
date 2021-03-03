using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using TomsConfig.Model;

namespace TomsConfig {
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class ConfigReader {
        private static IConfigFactory configFactory;

        public static IConfig ReadFile(string path, IConfigFactory factory) {
            configFactory = factory;
            return ReadFile(path);
        }

        public static IConfig ReadFile(string path) {
            if (!File.Exists(path))
                throw new FileNotFoundException("The file '" + path + "' was not found.");

            var s = File.ReadAllText(path);
            return Read(s);
        }

        public static IConfig Read(string s, IConfigFactory factory) {
            configFactory = factory;
            return Parse(s);
        }

        public static IConfig Read(string s) {
            configFactory ??= new ConfigFactory();
            return Parse(s);
        }

        private static IConfig Parse(string s) {
            var config = configFactory.CreateConfig();
            var block = configFactory.CreateBlock();
            var lines = GetLines(s);

            if (!IsBlock(lines[0], out var name))
                throw new FormatException("First line of config is not a block");

            for (var i = 1; i < lines.Length; i++) {
                var line = lines[i].Trim();
                if (IsBlock(line, out var n)) {
                    config.Add(name, block);
                    block = configFactory.CreateBlock();
                    name = n;
                }
                else if (IsItem(line, out var iName, out var item)) {
                    block.Add(iName, item);
                }
            }

            config.Add(name, block);

            return config;
        }

        private static bool IsBlock(string line, out string name) {
            name = null;

            if (!IsValidLine(line)) return false;
            if (line.First() != '[' || line.Last() != ']') return false;
            name = line.Substring(1, line.Length - 2);
            return true;
        }

        private static bool IsItem(string line, out string name, out IConfigItem item) {
            name = null;
            item = null;

            if (!IsValidLine(line)) return false;
            var split = line.Split('=');
            if (split.Length < 2 ||
                split[0].Trim().Length < 1 ||
                string.Join('=', split.Skip(1)).Trim().Length < 1) return false;

            name = split[0].Trim();
            item = configFactory.CreateItem(string.Join('=', split.Skip(1)).Trim());
            return true;
        }

        private static bool IsValidLine(string s) {
            return s.Length > 2;
        }

        private static string[] GetLines(string s) {
            return s.Split(
                new[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None
            );
        }
    }
}
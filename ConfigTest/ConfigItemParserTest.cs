using System;
using System.Collections.Generic;
using NUnit.Framework;
using TomsConfig;
using TomsConfig.Model;

namespace ConfigTest {
    public class ConfigItemParserTest {
        private const int ARRAY_SIZE = 2500;
        private readonly Random _r = new Random();

        private IConfig CreateConfig<T>(IReadOnlyList<T> array) {
            var configCode = "[test]\n";
            for (var i = 0; i < array.Count; i++) {
                configCode += i + " = " + array[i] + "\n";
            }

            return ConfigReader.Read(configCode);
        }

        private IConfig Setup<T>(T[] array, Func<T> func) {
            for (var i = 0; i < array.Length; i++) {
                array[i] = func();
            }

            return CreateConfig(array);
        }

        private IConfig CreateArrayConfig<T>(IReadOnlyList<T[]> array) {
            var configCode = "[test]\n";
            for (var i = 0; i < array.Count; i++) {
                configCode += i + " = [" + string.Join(", ", array[i]) + "]\n";
            }

            return ConfigReader.Read(configCode);
        }

        private IConfig SetupArray<T>(T[][] array, Func<T> func) {
            for (var i = 0; i < array.Length; i++) {
                array[i] = new[] {func(), func(), func()};
            }

            return CreateArrayConfig(array);
        }

        [Test]
        public void TestInteger() {
            var values = new int[ARRAY_SIZE];
            var config = Setup(values, () => _r.Next());

            for (var i = 0; i < values.Length; i++) {
                Assert.AreEqual(values[i], config["test"][i.ToString()].GetParser().GetInt());
            }
        }

        [Test]
        public void TestDouble() {
            var array = new double[ARRAY_SIZE];
            var config = Setup(array, () => _r.NextDouble() * 5000 - 2500);

            for (var i = 0; i < array.Length; i++) {
                Assert.AreEqual(array[i], config["test"][i.ToString()].GetParser().GetDouble());
            }
        }

        [Test]
        public void TestBool() {
            var array = new bool[ARRAY_SIZE];
            var config = Setup(array, () => _r.Next(2) == 1);

            for (var i = 0; i < array.Length; i++) {
                Assert.AreEqual(array[i], config["test"][i.ToString()].GetParser().GetBool());
            }
        }

        [Test]
        public void TestString() {
            var array = new string[ARRAY_SIZE];
            var config = Setup(array, () => "'" + _r.Next() + "'");

            for (var i = 0; i < array.Length; i++) {
                Assert.AreEqual(array[i], "'" + config["test"][i.ToString()].GetParser().GetString() + "'");
            }
        }

        [Test]
        public void TestIntArray() {
            var array = new int[ARRAY_SIZE][];
            var config = SetupArray(array, () => _r.Next());

            for (var i = 0; i < array.Length; i++) {
                Assert.AreEqual(array[i], config["test"][i.ToString()].GetParser().GetInts());
            }
        }

        [Test]
        public void TestDoubleArray() {
            var array = new double[ARRAY_SIZE][];
            var config = SetupArray(array, () => _r.NextDouble() + 5000 - 2500);

            for (var i = 0; i < array.Length; i++) {
                Assert.AreEqual(array[i], config["test"][i.ToString()].GetParser().GetDoubles());
            }
        }

        [Test]
        public void TestBoolArray() {
            var array = new bool[ARRAY_SIZE][];
            var config = SetupArray(array, () => _r.NextDouble() > .5);

            for (var i = 0; i < array.Length; i++) {
                Assert.AreEqual(array[i], config["test"][i.ToString()].GetParser().GetBools());
            }
        }

        [Test]
        public void TestStringArray() {
            var array = new string[ARRAY_SIZE][];
            var config = SetupArray(array, () => "'" + _r.Next() + "'");

            for (var i = 0; i < array.Length; i++) {
                var result = string.Join(", ", config["test"][i.ToString()].GetParser().GetStrings());
                Assert.AreEqual(string.Join(", ", array[i]).Replace("'", ""), result);
            }
        }

        [Test]
        public void TestWrongStringFormat() {
            var c = ConfigReader.Read(@"[test]
text = no quote");
            Assert.Throws(typeof(Exception), () => c["test"]["text"].GetParser().GetString());
            c = ConfigReader.Read(@"[test]
text = 'one quote");
            Assert.Throws(typeof(Exception), () => c["test"]["text"].GetParser().GetString());
            c = ConfigReader.Read(@"[test]
text = 'wrong quote""");
            Assert.Throws(typeof(Exception), () => c["test"]["text"].GetParser().GetString());
            c = ConfigReader.Read(@"[test]
text = 'good quote'");
            Assert.DoesNotThrow(() => c["test"]["text"].GetParser().GetString());
            c = ConfigReader.Read(@"[test]
text = ""good quote""");
            Assert.DoesNotThrow(() => c["test"]["text"].GetParser().GetString());
        }
    }
}
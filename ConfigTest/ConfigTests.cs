using System;
using NUnit.Framework;
using TomsConfig;
using TomsConfig.Model;

namespace ConfigTest {
    public class ConfigTests {
        private IConfig _config;
        
        [SetUp]
        public void Setup() {
            _config = ConfigReader.Read(@"[test1]
n1 = 12
n2 = 12
n3 = 13

[test2]
b1 = true
b2 = true
b3 = false

[test3]
s1 = 'text'
s2 = 'text'
s3 = 'other text'");
        }

        [Test]
        public void TestConfigWrongBlock() {
            Assert.DoesNotThrow(() => _config["test1"]["n1"].GetInt());
            Assert.Throws(typeof(InvalidOperationException), () => _config["wrong"]["n1"].GetInt());
        }

        [Test]
        public void TestConfigOverwrite() {
            _config = ConfigReader.Read(@"[test]
a = 1
a = 2
a = 3");
            Assert.AreEqual(3, _config["test"]["a"].GetInt());
        }

        [Test]
        public void TestConfigBlockOverwrite() {
            _config = ConfigReader.Read(@"[test]
a = 1
[test]
b = 1");
            Assert.Throws(typeof(InvalidOperationException), () => _config["test"]["a"].GetInt());
            Assert.AreEqual(1, _config["test"]["b"].GetInt());
        }

        [Test]
        public void TestGetBlocks() {
            CollectionAssert.AreEquivalent(new[] {"test1", "test2", "test3"}, _config.GetBlocks());
        }
        
        [Test]
        public void TestGetItems() {
            CollectionAssert.AreEquivalent(new[] {"n1", "n2", "n3"}, _config["test1"].GetItems());
            CollectionAssert.AreEquivalent(new[] {"b1", "b2", "b3"}, _config["test2"].GetItems());
            CollectionAssert.AreEquivalent(new[] {"s1", "s2", "s3"}, _config["test3"].GetItems());
        }
    }
}
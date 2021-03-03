using NUnit.Framework;
using TomsConfig;
using TomsConfig.Model;

namespace ConfigTest {
    public class ConfigItemEquityTest {
        private IConfig _config;

        [SetUp]
        public void Setup() {
            _config = ConfigReader.Read(@"[test]
n1 = 12
n2 = 12
n3 = 13

b1 = true
b2 = true
b3 = false

s1 = 'text'
s2 = 'text'
s3 = 'other text'");
        }

        [Test]
        public void EqualsTest() {
            Assert.AreEqual(true, _config["test"]["n1"].Equals(_config["test"]["n2"]));
            Assert.AreEqual(true, _config["test"]["b1"].Equals(_config["test"]["b2"]));
            Assert.AreEqual(true, _config["test"]["s1"].Equals(_config["test"]["s2"]));
        }

        [Test]
        public void NotEqualTest() {
            Assert.AreEqual(false, _config["test"]["n1"].Equals(_config["test"]["n3"]));
            Assert.AreEqual(false, _config["test"]["b1"].Equals(_config["test"]["b3"]));
            Assert.AreEqual(false, _config["test"]["s1"].Equals(_config["test"]["s3"]));
            Assert.AreEqual(false, _config["test"]["n2"].Equals(_config["test"]["n3"]));
            Assert.AreEqual(false, _config["test"]["b2"].Equals(_config["test"]["b3"]));
            Assert.AreEqual(false, _config["test"]["s2"].Equals(_config["test"]["s3"]));
        }
    }
}
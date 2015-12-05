using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Pancakes;
using Pancakes.Model;

namespace UnitTestLibrary
{
    [TestClass]
    public class ExtensionTest
    {
        [TestMethod]
        public void ToNumericalStringTestIpFalseFpFalse()
        {
            Assert.AreEqual("0", 0.0m.ToNumericalString());
            Assert.AreEqual("0", 0m.ToNumericalString());
        }

        [TestMethod]
        public void ToNumericalStringTestIpFalseFpTrue()
        {
            Assert.AreEqual("0", 0.0m.ToNumericalString());
            Assert.AreEqual("1/2", 0.5m.ToNumericalString());
            Assert.AreEqual("1/4", 0.25m.ToNumericalString());
        }

        [TestMethod]
        public void ToNumericalStringTestIpTrueFpFalse()
        {
            Assert.AreEqual("1", 1.0m.ToNumericalString());
            Assert.AreEqual("99", 99m.ToNumericalString());
        }

        [TestMethod]
        public void ToNumericalStringTestIpTrueFpTrue()
        {
            Assert.AreEqual("1 and 1/2", 1.5m.ToNumericalString());
            Assert.AreEqual("99 and 77/100", 99.77m.ToNumericalString());
        }

        [TestMethod]
        public void ToNumericalStringLargePersion()
        {
            Assert.AreEqual("1 and 1/2", 1.5m.ToNumericalString());
            Assert.AreEqual("99 and 77/100", 99.77m.ToNumericalString());
        }
    }
}
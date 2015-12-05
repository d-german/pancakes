using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Pancakes.Model;

namespace UnitTestLibrary
{
    [TestClass]
    public class VolumnAmountResultTest
    {
        private static readonly VolumnAmountResult _sut = new VolumnAmountResult {NameU1 = "U1", NameU2 = "U2"};

        [TestMethod]
        public void ZeroU1AndZeroU2Test()
        {
            AssertCorrectVolumnAmountResult("0", 0m, 0m);
        }

        [TestMethod]
        public void ZeroU1AndLessThanEqualToOneU2Test()
        {
            AssertCorrectVolumnAmountResult("1 U2", 0, 1);
            AssertCorrectVolumnAmountResult("1/4 U2", 0m, 0.25m);
        }

        [TestMethod]
        public void ZeroU1AndGreaterThanEqualToOneU2Test()
        {
            AssertCorrectVolumnAmountResult("1 and 1/4 U2s", 0, 1.25m);
        }

        [TestMethod]
        public void LessThanEqualToOneU1AndZeroU2Test()
        {
            AssertCorrectVolumnAmountResult("1/2 U1", .5m, 0);
            AssertCorrectVolumnAmountResult("1 U1", 1m, 0);
        }

        [TestMethod]
        public void LessThanEqualToOneU1AndLessThanEqualToOneU2Test()
        {
            AssertCorrectVolumnAmountResult("1 U1 and 1 U2", 1m, 1m);
            AssertCorrectVolumnAmountResult("1/2 U1 and 1 U2", 0.5m, 1m);
            AssertCorrectVolumnAmountResult("1/2 U1 and 1/4 U2", 0.5m, 0.25m);
        }

        [TestMethod]
        public void LessThanEqualToOneU1AndGreaterThanOneU2Test()
        {
            AssertCorrectVolumnAmountResult("1 U1 and 3 U2s", 1m, 3);
            AssertCorrectVolumnAmountResult("1/2 U1 and 3 U2s", 0.5m, 3m);
            AssertCorrectVolumnAmountResult("1/2 U1 and 3 and 1/4 U2s", 0.5m, 3.25m);
        }

        [TestMethod]
        public void GreaterThanOneU1AndZeroU2Test()
        {
            AssertCorrectVolumnAmountResult("2 U1s", 2, 0);
            AssertCorrectVolumnAmountResult("2 and 1/2 U1s", 2.5m, 0);
        }

        [TestMethod]
        public void GreaterThanOneU1AndLessThanEqualToOneU2Test()
        {
            AssertCorrectVolumnAmountResult("2 U1s and 1/4 U2", 2, .25m);
            AssertCorrectVolumnAmountResult("2 and 1/2 U1s and 1/4 U2", 2.5m, 0.25m);
        }

        [TestMethod]
        public void GreaterThanOneU1AndGreaterThanOneU2Test()
        {
            AssertCorrectVolumnAmountResult("2 U1s and 2 U2s", 2, 2);
            AssertCorrectVolumnAmountResult("2 U1s and 2 and 1/4 U2s", 2, 2.25m);
            AssertCorrectVolumnAmountResult("2 and 1/2 U1s and 3 and 1/4 U2s", 2.5m, 3.25m);
        }

        private static void AssertCorrectVolumnAmountResult(string expectedResult, decimal numU1, decimal numU2)
        {
            if (_sut != null) Assert.AreEqual(expectedResult, _sut.GetVolumnAmount(numU1, numU2));
        }
    }
}
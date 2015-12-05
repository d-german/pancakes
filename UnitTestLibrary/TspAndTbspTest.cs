using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Pancakes.Model;

namespace UnitTestLibrary
{
    [TestClass]
    public class TspAndTbspTest
    {
        private static TspAndTbsp CreateTspAndTbsp(decimal numTsp)
        {
            return new TspAndTbsp(new Tsp(numTsp));
        }

        [TestMethod]
        public void LessthanThreeTspTest()
        {
            VerifyTspAndTbspCount(expectedTsp: 0, expectedTbsp: 0, numTsp: 0);
            VerifyTspAndTbspCount(expectedTsp: 1, expectedTbsp: 0, numTsp: 1);
            VerifyTspAndTbspCount(expectedTsp: 2, expectedTbsp: 0, numTsp: 2);
        }

        [TestMethod]
        public void EqualToThreeTspTest()
        {
            VerifyTspAndTbspCount(expectedTsp: 0, expectedTbsp: 1, numTsp: 3);
        }

        [TestMethod]
        public void GreaterThanThreeTspTest()
        {
            VerifyTspAndTbspCount(expectedTsp: 1, expectedTbsp: 1, numTsp: 4);
            VerifyTspAndTbspCount(expectedTsp: 2, expectedTbsp: 3, numTsp: 11);
        }

        private static void VerifyTspAndTbspCount(decimal expectedTsp, decimal expectedTbsp, decimal numTsp)
        {
            var sut = CreateTspAndTbsp(numTsp);
            Assert.AreEqual(expectedTsp, sut.CalculatedTsps);
            Assert.AreEqual(expectedTbsp, sut.CalculatedTbsps);
        }
    }
}
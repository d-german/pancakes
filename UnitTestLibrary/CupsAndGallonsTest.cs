using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Pancakes.Model;

namespace UnitTestLibrary
{
    [TestClass]
    public class CupsAndGallonsTest
    {
        [TestMethod]
        public void NoCups()
        {
            AssertCorrectCupsAnGallons(expectedGals: 0m, expectedCups: 0m, numCups: 0m);
        }

        [TestMethod]
        public void LessThanOneCup()
        {
            AssertCorrectCupsAnGallons(expectedGals: 0m, expectedCups: 0.5m, numCups: 0.5m);
        }

        [TestMethod]
        public void OneCup()
        {
            AssertCorrectCupsAnGallons(expectedGals: 0m, expectedCups: 1m, numCups: 1m);
        }

        [TestMethod]
        public void OneAndOneHalfCup()
        {
            AssertCorrectCupsAnGallons(expectedGals: 0m, expectedCups: 1.5m, numCups: 1.5m);
        }

        [TestMethod]
        public void TwoAndOneHalfCup()
        {
            AssertCorrectCupsAnGallons(expectedGals: 0m, expectedCups: 2.5m, numCups: 2.5m);
        }

        [TestMethod]
        public void OneHalfGallon()
        {
            AssertCorrectCupsAnGallons(expectedGals: 0.5m, expectedCups: 0m, numCups: 8m);
        }

        [TestMethod]
        public void MoreThanHalfLessThanOneGallon()
        {
            AssertCorrectCupsAnGallons(expectedGals: 0.5m, expectedCups: 6m, numCups: 14m);
        }

        [TestMethod]
        public void MultipleWholeGallons()
        {
            AssertCorrectCupsAnGallons(expectedGals: 1m, expectedCups: 0m, numCups: 16m);
            AssertCorrectCupsAnGallons(expectedGals: 2m, expectedCups: 0m, numCups: 32m);
        }

        [TestMethod]
        public void MultipleWholeGallonsAndCups()
        {
            AssertCorrectCupsAnGallons(expectedGals: 2m, expectedCups: 2m, numCups: 34m);
        }

        [TestMethod]
        public void MultipleFractionalGallonsAndCups()
        {
            AssertCorrectCupsAnGallons(expectedGals: 2m, expectedCups: 2.25m, numCups: 34.25m);
            AssertCorrectCupsAnGallons(expectedGals: 1m, expectedCups: 5.3m, numCups: 21.30m);
        }

        private static CupsAndGallons CreateCupsAndGallons(decimal numCups)
        {
            return new CupsAndGallons(new Cups(numCups));
        }

        private static void AssertCorrectCupsAnGallons(decimal expectedGals, decimal expectedCups, decimal numCups)
        {
            var sut = CreateCupsAndGallons(numCups);
            Assert.AreEqual(expectedGals, sut.CalculatedGals);
            Assert.AreEqual(expectedCups, sut.CalculatedCups);
        }
    }
}
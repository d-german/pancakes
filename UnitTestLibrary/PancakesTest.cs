using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Pancakes.Model;

namespace UnitTestLibrary
{
    [TestClass]
    public class PancakesTest
    {
        [TestMethod]
        public void CalcNumDozenTest()
        {
            Assert.AreEqual(0, Eggs.CalcNumDozen(6));

            Assert.AreEqual(1, Eggs.CalcNumDozen(12));

            Assert.AreEqual(2, Eggs.CalcNumDozen(25));
        }

        [TestMethod]
        public void GetQuantityTest()
        {
            Assert.AreEqual(
                "1 egg",
                Eggs.GetQuantity(1));

            Assert.AreEqual(
                "2 eggs",
                Eggs.GetQuantity(2));

            Assert.AreEqual(
                "11 eggs",
                Eggs.GetQuantity(11));

            Assert.AreEqual(
                "1 dozen",
                Eggs.GetQuantity(12));

            Assert.AreEqual(
                "1 dozen and 1 egg",
                Eggs.GetQuantity(13));

            Assert.AreEqual(
                "1 dozen and 2 eggs",
                Eggs.GetQuantity(14));

            Assert.AreEqual(
                "8 dozen and 1 egg",
                Eggs.GetQuantity(97));

            Assert.AreEqual(
                "8 dozen and 3 eggs",
                Eggs.GetQuantity(99));
        }
    }


    [TestClass]
    public class ButtermilkTest
    {
        [TestMethod]
        public void CalcNumDozenTest()
        {
            Assert.AreEqual(0, Eggs.CalcNumDozen(6));

            Assert.AreEqual(1, Eggs.CalcNumDozen(12));

            Assert.AreEqual(2, Eggs.CalcNumDozen(25));
        }
    }
}
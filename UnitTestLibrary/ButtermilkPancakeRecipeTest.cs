using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Pancakes;
using Pancakes.Model;

namespace UnitTestLibrary
{
    [TestClass]
    public class ButtermilkPancakeRecipeTest
    {
        [TestMethod]
        public void GetFlOz()
        {
            var sut = new ButtermilkPancakeRecipe();
            var expected = "( 184 fl oz )".Trim();
            var actual = sut.GetFlOz(23).Trim();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetFlourAmountTest()
        {
            var sut = new ButtermilkPancakeRecipe(25);
            var expected = "4 cups 2 tbsps and 2 tsps ( 1.3 lbs )".Trim();
            var actual = sut.GetFlourAmount().Trim();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetOilAmountTest()
        {
            var sut = new ButtermilkPancakeRecipe(25);
            var expected = "1 cup 2 tsps ( 8.3 fl oz )".Trim();
            var actual = sut.GetOilAmount().Trim();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPoundsFlourAmountTest()
        {
            var expected = "( 8.0 lbs )".Trim();
            var actual = ButtermilkPancakeRecipe.GetPoundsFlourAmount(25).Trim();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCupsAmountTest()
        {
            var testData = new[]
                               {
                                   new {NumCups = 1.25m, Expected = "1 and 1/4 cups"},
                                   new {NumCups = 1.24m, Expected = "1 cup 3 tbsps and 2 and 1/2 tsps"},
                                   new {NumCups = 2m, Expected = "2 cups"},
                                   new {NumCups = 1m, Expected = "1 cup"},
                                   new {NumCups = 5.13m, Expected = "5 cups 2 tbsps and 1/4 tsp"},
                               };

            var sut = new ButtermilkPancakeRecipe();

            foreach (var data in testData)
            {
                var actual = sut.GetCupsAmount(data.NumCups);
                Assert.AreEqual(actual, data.Expected);
            }
        }

        [TestMethod]
        public void GetTspsAmountTest()
        {
            var testData = new[]
                               {
                                   new {NumTsps = 1.25m, Expected = "1 and 1/4 tsps"},
                                   new {NumTsps = 47m, Expected = "3/4 cup 3 tbsps and 2 tsps"},
                                   new {NumTsps = 320.25m, Expected = "6 and 1/2 cups 2 tbsps and 2 and 1/4 tsps"},
                                   new {NumTsps = 28m, Expected = "1/2 cup 1 tbsp and 1 tsp"},
                                   new {NumTsps = 1m, Expected = "1 tsp"}
                               };

            var sut = new ButtermilkPancakeRecipe();
            const string msg = "\nFor {0} tsps\nExpected: {1}\nActual: {2}";


            foreach (var data in testData)
            {
                var d = data;
                var actual = sut.GetTspsAmount(d.NumTsps);
                Assert.AreEqual(actual, d.Expected, string.Format(msg, d.NumTsps, d.Expected, actual));
            }
        }


        [TestMethod]
        public void ToTspTest()
        {
            var cups = new Cups(0.5m);
            var tsps = ButtermilkPancakeRecipe.ToTsp(cups);
            Assert.AreEqual(24m, tsps.Value);
        }

        [TestMethod]
        public void GetEggsAmountTest()
        {
            var sut = new ButtermilkPancakeRecipe(50);
            var actualEggsAmount = sut.GetEggsAmount();
            Assert.AreEqual("8 eggs", actualEggsAmount);
        }

        [TestMethod]
        public void GetBakingSodaAmountTest()
        {
            var sut = new ButtermilkPancakeRecipe(50);
            var actualResult = sut.GetBakingSodaAmount();
            Assert.AreEqual("2 tbsps and 2 and 1/3 tsps", actualResult);
        }

        [TestMethod]
        public void GetBakingPowderAmountTest()
        {
            var sut = new ButtermilkPancakeRecipe(50);
            var actualResult = sut.GetBakingPowderAmount();
            Assert.AreEqual("2 tbsps and 2 and 1/3 tsps", actualResult);
        }

        [TestMethod]
        public void GetCupsButtermilkAmountTest()
        {
            var sut = new ButtermilkPancakeRecipe(98);
            var actualResult = sut.GetButtermilkAmount();
            Assert.AreEqual("1 gallon and 1/3 cup", actualResult);
        }

        [TestMethod]
        public void GetCupsSugerAmountTest()
        {
            var sut = new ButtermilkPancakeRecipe(1000);
            var actualResult = sut.GetSugerAmount();
            Assert.AreEqual("2 and 1/2 gallons and 1 and 2/3 cups ( 22.62 lbs )", actualResult);
        }

        [TestMethod]
        public void TestNumEggs()
        {
            const decimal expectedNumEggs = 25m/3m;
            var actualNumEggs = new ButtermilkPancakeRecipe(50).NumEggs;
            Assert.AreEqual(expectedNumEggs.Round(), actualNumEggs.Round());
        }

        [TestMethod]
        public void TestNumCupsButtermilk()
        {
            const decimal expectedCupsButterMilk = 25m/3m;
            var actualCupsButtermilk = new ButtermilkPancakeRecipe(50).NumCupsButtermilk;
            Assert.AreEqual(expectedCupsButterMilk.Round(), actualCupsButtermilk.Round());
        }

        [TestMethod]
        public void TestNumCupsOil()
        {
            const decimal expectedCupsOil = 2.125m; // 2.083333333
            var actualCupsOil = new ButtermilkPancakeRecipe(51).NumCupsOil;
            Assert.AreEqual(expectedCupsOil.Round(), actualCupsOil.Round());
        }

        [TestMethod]
        public void TestNumCupsOilByThrees()
        {
            const decimal expectedCupsOil = 0.25m;
            var actualCupsOil = new ButtermilkPancakeRecipe(6).NumCupsOil;
            Assert.AreEqual(expectedCupsOil.Round(), actualCupsOil.Round());
        }
    }
}
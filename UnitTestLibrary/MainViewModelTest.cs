using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Pancakes;
using Pancakes.Messages;
using Pancakes.ViewModel;
using System.Linq;

namespace UnitTestLibrary
{
    [TestClass]
    public class MainViewModelTest
    {

        [TestMethod]
        public void MessageTest()
        {
            var sut = new MainViewModel { NumPancakes = 25 };
            sut.OnNumPancakesChanged(new NumPancakesChangedMsg { NumPancakes = sut.NumPancakes });
           
        }


        [TestMethod]
        public void OnNumPancakesChangedTest()
        {
            var sut = new MainViewModel {NumPancakes = 25};
            sut.OnNumPancakesChanged(new NumPancakesChangedMsg{NumPancakes = sut.NumPancakes});
        }


        public void AssertEmpty(string value)
        {
            Assert.AreEqual(null, value);
        }


    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pancakes.Model;


namespace PancakeTests
{
    [TestClass]
    public class EggsTest
    {
        [TestMethod]
        public void CalcNumDozenTest()
        {
            int numDozen = Pancakes.Model.Eggs.CalcNumDozen(12);
        }
    }
}

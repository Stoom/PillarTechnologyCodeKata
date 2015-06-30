using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;
using VendingMachine.Products;

namespace VentingMachine.Test
{
    [TestClass]
    public class SelectProductTest
    {
        private CoinManager _coinMgr;
        private ProductManager _prodMgr;

        [TestInitialize]
        public void Init()
        {
            _coinMgr = new CoinManager();
            _prodMgr = new ProductManager(_coinMgr);
        }

        [TestMethod]
        public void SelectColaForOneDollarWithOneDollar()
        {
            // Insert 4 quarters
            for (var i = 0; i < 4; i++) 
                _coinMgr.Insert(Coins.Quarter);

            // Buy cola
            var product = _prodMgr.Buy("Cola");
            Assert.AreEqual(typeof(Cola), product.GetType());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectColaForOneDollareWithFiftyCents()
        {
            // Insert 4 quarters
            for (var i = 0; i < 2; i++)
                _coinMgr.Insert(Coins.Quarter);

            // Buy cola
            var product = _prodMgr.Buy("Cola");
            Assert.AreEqual(typeof(Cola), product.GetType());
        }

        [TestMethod]
        public void SelectChipsForFiftyCentsWithFiftyCents()
        {
            // Insert 4 quarters
            for (var i = 0; i < 2; i++)
                _coinMgr.Insert(Coins.Quarter);

            // Buy cola
            var product = _prodMgr.Buy("Chips");
            Assert.AreEqual(typeof(Chips), product.GetType());
        }

        [TestMethod]
        public void SelectCandyForSixtyFiveCentsWithSixtyFiveCents()
        {
            // Insert 4 quarters, a dime, and a nickle ($0.65)
            for (var i = 0; i < 2; i++)
                _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Dime);
            _coinMgr.Insert(Coins.Nickel);

            // Buy cola
            var product = _prodMgr.Buy("Candy");
            Assert.AreEqual(typeof(Candy), product.GetType());
        }
    }
}

using System;
using System.Collections.Generic;
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
        private DisplayManager _dispMgr;

        [TestInitialize]
        public void Init()
        {
            _dispMgr = new DisplayManager();
            _coinMgr = new CoinManager(_dispMgr);
            _prodMgr = new ProductManager(_coinMgr, _dispMgr);
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

        [TestMethod]
        public void DisplayThankYouAfterDispensedAndInsertCoins()
        {
            var receivedEvents = new List<string>();
            _dispMgr.DisplayUpdate += delegate(object sender, DisplayUpdateEventArgs e) {
                receivedEvents.Add(e.Message);
            };

            _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Quarter);
            _prodMgr.Buy("Chips");

            Assert.AreEqual(4, receivedEvents.Count);
            Assert.AreEqual("THANK YOU", receivedEvents[2]);
            Assert.AreEqual("INSERT COIN", receivedEvents[3]);
        }
    }
}

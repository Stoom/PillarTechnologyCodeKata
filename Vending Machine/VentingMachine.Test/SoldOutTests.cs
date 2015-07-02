using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VentingMachine.Test
{
    [TestClass]
    public class SoldOutTests
    {
        private ICurrencyManager<Coins> _coinMgr;
        private IProductManager _prodMgr;
        private IDisplayManager _dispMgr;

        [TestInitialize]
        public void Init()
        {
            _dispMgr = new DisplayManager();
            _coinMgr = new CoinManager(_dispMgr);
            _prodMgr = new ProductManager((ICurrencyManager)_coinMgr, _dispMgr);
        }

        [TestMethod]
        public void PrintSoldOutWhenBuyingThreeOfTwoColas()
        {
            // Read display
            var receivedEvents = new List<string>();
            _dispMgr.DisplayUpdate += delegate(object sender, DisplayUpdateEventArgs e)
            {
                receivedEvents.Add(e.Message);
            };

            for (var i = 0; i < 3; i++)
            {
                // Reset reading display
                receivedEvents = new List<string>();

                // Insert 4 quarters
                for (var ii = 0; ii < 4; ii++)
                    _coinMgr.Insert(Coins.Quarter);
                // Buy cola
                _prodMgr.Buy("Cola");
            }

            // Test
            Assert.AreEqual("SOLD OUT",receivedEvents[4]);
        }

        [TestMethod]
        public void PrintCurrentAmountOfMoneyAfterSoldOut()
        {
            // Read display
            var receivedEvents = new List<string>();
            _dispMgr.DisplayUpdate += delegate(object sender, DisplayUpdateEventArgs e)
            {
                receivedEvents.Add(e.Message);
            };

            for (var i = 0; i < 3; i++)
            {
                // Reset reading display
                receivedEvents = new List<string>();

                // Insert 4 quarters
                for (var ii = 0; ii < 4; ii++)
                    _coinMgr.Insert(Coins.Quarter);
                // Buy cola
                _prodMgr.Buy("Cola");
            }

            // Test
            Assert.AreEqual("SOLD OUT", receivedEvents[4]);
            Assert.AreEqual("$1.00", receivedEvents[5]);
        }

        [TestMethod]
        public void BuyOtherItemAfterSoldOutItem()
        {
            // Read display
            var receivedEvents = new List<string>();
            _dispMgr.DisplayUpdate += delegate(object sender, DisplayUpdateEventArgs e)
            {
                receivedEvents.Add(e.Message);
            };

            // Buy 2 candy then chips
            for (var i = 0; i < 2; i++)
            {
                // Reset reading display
                receivedEvents = new List<string>();
                // Insert coins
                _coinMgr.Insert(Coins.Quarter);
                _coinMgr.Insert(Coins.Quarter);
                _coinMgr.Insert(Coins.Dime);
                _coinMgr.Insert(Coins.Nickel);
                // Buy food
                _prodMgr.Buy("Candy");
            }
            // Test for Sold Out
            Assert.AreEqual("SOLD OUT", receivedEvents[4]);
            // Buy a different food
            var product = _prodMgr.Buy("Chips");
            // Check if we got a product
            Assert.IsNotNull(product);
        }
    }
}

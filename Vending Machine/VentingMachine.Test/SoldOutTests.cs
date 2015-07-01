using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VentingMachine.Test
{
    [TestClass]
    public class SoldOutTests
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
    }
}

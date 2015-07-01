using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VentingMachine.Test
{
    [TestClass]
    public class ReturnCoins
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
        public void InsertTwoQuartersOneDimeOneNickelReturnSame()
        {
            _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Dime);
            _coinMgr.Insert(Coins.Nickel);

            var coins = _coinMgr.ReturnCoins();

            var expectedResults = new Dictionary<Coins, int>
            {
                {Coins.Quarter, 2},
                {Coins.Dime, 1},
                {Coins.Nickel, 1}
            };
            CollectionAssert.AreEqual(expectedResults, coins);
        }

        [TestMethod]
        public void ReturnCoinsAndDisplayInsertCoin()
        {
            // Insert coins
            _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Quarter);

            // Read display
            var receivedEvents = new List<string>();
            _dispMgr.DisplayUpdate += delegate(object sender, DisplayUpdateEventArgs e)
            {
                receivedEvents.Add(e.Message);
            };

            // Return coins
            _coinMgr.ReturnCoins();

            // Test results
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("INSERT COIN", receivedEvents[0]);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VentingMachine.Test
{
    [TestClass]
    public class ReturnCoinsTest
    {
        private ICurrencyManager<Coins> _coinMgr;
        private IDisplayManager _dispMgr;

        [TestInitialize]
        public void Init()
        {
            _dispMgr = new DisplayManager();
            _coinMgr = new CoinManager(_dispMgr);
        }

        [TestMethod]
        public void InsertTwoQuartersOneDimeOneNickelReturnSame()
        {
            _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Dime);
            _coinMgr.Insert(Coins.Nickel);

            var coins = _coinMgr.ReturnCurrency();

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
            _coinMgr.ReturnCurrency();

            // Test results
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("INSERT COIN", receivedEvents[0]);
        }

        [TestMethod]
        public void DoNotReturnChangeMultipleTimes()
        {
            _coinMgr.Insert(Coins.Quarter);

            var coins = _coinMgr.ReturnCurrency();

            var expectedResults = new Dictionary<Coins, int>
            {
                {Coins.Quarter, 1},
            };
            CollectionAssert.AreEqual(expectedResults, coins);
            CollectionAssert.AreEqual(new Dictionary<Coins, int>(),  _coinMgr.ReturnCurrency());
        }
    }
}

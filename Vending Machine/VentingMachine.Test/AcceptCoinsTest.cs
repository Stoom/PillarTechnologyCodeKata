using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VentingMachine.Test
{
    [TestClass]
    public class AcceptCoinsTest
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
        public void AcceptNickels()
        {
            _coinMgr.Insert(Coins.Nickel);
            Assert.AreEqual("$0.05", _coinMgr.CurrentAmount);
        }

        [TestMethod]
        public void AcceptDimes()
        {
            _coinMgr.Insert(Coins.Dime);
            Assert.AreEqual("$0.10", _coinMgr.CurrentAmount);
        }

        [TestMethod]
        public void AcceptQuarters()
        {
            _coinMgr.Insert(Coins.Quarter);
            Assert.AreEqual("$0.25", _coinMgr.CurrentAmount);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void RejectInvalidCoins()
        {
            _coinMgr.Insert(Coins.Penny);
            Assert.AreEqual("$0.00", _coinMgr.CurrentAmount);
        }

        [TestMethod]
        public void NoCoinsInsertCoinMessage()
        {
            Assert.AreEqual("INSERT COIN", _coinMgr.CurrentAmount);
        }

        [TestMethod]
        public void UpdateCurrentAmountOnInsert()
        {
            var receivedEvents = new List<string>();
            _dispMgr.DisplayUpdate += delegate(object sender, DisplayUpdateEventArgs e)
            {
                receivedEvents.Add(e.Message);
            };

            _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Quarter);

            Assert.AreEqual("$0.25", receivedEvents[0]);
            Assert.AreEqual("$0.50", receivedEvents[1]);
        }
    }
}

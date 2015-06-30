using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VentingMachine.Test
{
    [TestClass]
    public class AcceptCoinsTest
    {
        private CoinManager _coinMgr;

        [TestInitialize]
        public void Init()
        {
            _coinMgr = new CoinManager();
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
    }
}

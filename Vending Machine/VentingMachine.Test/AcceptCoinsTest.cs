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
    }
}

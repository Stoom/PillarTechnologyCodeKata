using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VentingMachine.Test
{
    [TestClass]
    public class AcceptCoinsTest
    {
        private CoinManager coinMgr;

        [TestInitialize]
        public void Init()
        {
            coinMgr = new CoinManager();
        }

        [TestMethod]
        public void AcceptNickels()
        {
            coinMgr.Insert(Coins.Nickel);
            Assert.AreEqual("$0.05", coinMgr.CurrentAmount);
        }
    }
}

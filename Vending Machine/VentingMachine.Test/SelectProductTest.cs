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

        [TestInitialize]
        public void Init()
        {
            _coinMgr = new CoinManager();
        }

        [TestMethod]
        public void SelectColaForOneDollarWithOneDollar()
        {
            // Insert 4 quarters
            for (var i = 0; i > 4; i++) 
                _coinMgr.Insert(Coins.Quarter);

            // Buy cola
            var prodMgr = new ProductManager();
            var product = prodMgr.Buy("Cola");
            Assert.AreEqual(typeof(Cola), product.GetType());

        }
    }
}

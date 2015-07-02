using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VentingMachine.Test
{
    [TestClass]
    public class MakeChangeTest
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
        public void BuyCandyWithOneDollarFiftyFiveCentsAndMakeChange()
        {
            // Put in $1.55
            for (var i = 0; i < 6; i++)
                _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Nickel);

            // Listen for change to drop
            var receivedChange = new Dictionary<Coins, int>();
            _coinMgr.CurrencyDispensed += delegate
            {
                // Get Change
                receivedChange = _coinMgr.GetChange();
            };

            // Buy candy
            _prodMgr.Buy("Candy");

            // Test change returned
            var expectedResults = new Dictionary<Coins, int>
            {
                {Coins.Quarter, 3},
                {Coins.Dime, 1},
                {Coins.Nickel, 1}
            };
            CollectionAssert.AreEqual(expectedResults, receivedChange);
        }

        [TestMethod]
        public void DontMakeChangeTwice()
        {
            // Put in $1.55
            for (var i = 0; i < 6; i++)
                _coinMgr.Insert(Coins.Quarter);
            _coinMgr.Insert(Coins.Nickel);

            // Listen for change to drop
            var receivedChange = new Dictionary<Coins, int>();
            _coinMgr.CurrencyDispensed += delegate
            {
                // Get Change
                receivedChange = _coinMgr.GetChange();
            };

            // Buy candy
            _prodMgr.Buy("Candy");

            // Test change returned
            var expectedResults = new Dictionary<Coins, int>
            {
                {Coins.Quarter, 3},
                {Coins.Dime, 1},
                {Coins.Nickel, 1}
            };
            CollectionAssert.AreEqual(expectedResults, receivedChange);
            CollectionAssert.AreEqual(new Dictionary<Coins, int>(), _coinMgr.GetChange());
        }

        [TestMethod]
        public void BuyColaWithOneDollarAndDoNotMakeChange()
        {
            // Put in $1.55
            for (var i = 0; i < 4; i++)
                _coinMgr.Insert(Coins.Quarter);


            // Buy cola
            _prodMgr.Buy("Cola");

            // Test change returned
            CollectionAssert.AreEqual(new Dictionary<Coins, int>(), _coinMgr.GetChange());
        }
    }
}

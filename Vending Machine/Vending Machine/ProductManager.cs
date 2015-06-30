using System;
using System.Collections.Generic;
using VendingMachine.Products;

namespace VendingMachine
{
    public class ProductManager
    {
        private readonly CoinManager _coinManager;
        private readonly DisplayManager _dispManager;
        private readonly Dictionary<string, IProduct> _avliableProducts = new Dictionary<string, IProduct>
        {
            {"COLA", new Cola()},
            {"CHIPS", new Chips()},
            {"CANDY", new Candy()}
        };

        public ProductManager(CoinManager coinManager, DisplayManager displayManager)
        {
            if (coinManager == null)
                throw new ArgumentNullException("coinManager");
            if (displayManager == null)
                throw new ArgumentNullException("displayManager");

            _coinManager = coinManager;
            _dispManager = displayManager;
        }

        public IProduct Buy(string product)
        {
            var reqestedProduct = product.ToUpperInvariant();

            if (_avliableProducts.ContainsKey(reqestedProduct) &&
                _avliableProducts[reqestedProduct].Price == _coinManager.CurrentAmount)
            {
                _dispManager.OnDisplayUpdate(new DisplayUpdateEventArgs { Message = "THANK YOU" });
                _coinManager.ResetCurrentAmount();
                return _avliableProducts[reqestedProduct];
            }
            throw new ArgumentException("Product is not found");
        }
    }
}

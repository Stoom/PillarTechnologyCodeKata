using System;
using System.Collections.Generic;
using VendingMachine.Products;

namespace VendingMachine
{
    public class ProductManager
    {
        private readonly CoinManager _coinManager;
        private readonly Dictionary<string, IProduct> _avliableProducts = new Dictionary<string, IProduct>
        {
            {"COLA", new Cola()},
            {"CHIPS", new Chips()},
            {"CANDY", new Candy()}
        };

        public ProductManager(CoinManager coinManager)
        {
            _coinManager = coinManager;
        }

        public IProduct Buy(string product)
        {
            var reqestedProduct = product.ToUpperInvariant();
            if (_avliableProducts.ContainsKey(reqestedProduct) &&
                _avliableProducts[reqestedProduct].Price == _coinManager.CurrentAmount)
            {
                return _avliableProducts[reqestedProduct];
            }
            throw new ArgumentException("Product is not found");
        }
    }
}

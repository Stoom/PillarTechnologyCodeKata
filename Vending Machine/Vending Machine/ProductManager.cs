using System;
using System.Collections.Generic;
using System.Globalization;
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

            try
            {
                IProduct dispensedProduct = null;

                if (_avliableProducts.ContainsKey(reqestedProduct) &&
                    !_avliableProducts[reqestedProduct].IsOutOfStock)
                {
                    _coinManager.Subtract(_avliableProducts[reqestedProduct].Price);
                    _avliableProducts[reqestedProduct].Inventory--;
                    _dispManager.OnDisplayUpdate(new DisplayUpdateEventArgs { Message = "THANK YOU" });
                    _coinManager.DisplayCurrentAmount();
                    dispensedProduct = _avliableProducts[reqestedProduct];
                }
                else if (_avliableProducts[reqestedProduct].IsOutOfStock)
                {
                    _dispManager.OnDisplayUpdate(new DisplayUpdateEventArgs{ Message = "SOLD OUT" });
                    _coinManager.DisplayCurrentAmount();
                }
                else
                {
                    throw new ArgumentException("Product is not found");
                }

                return dispensedProduct;
            }
            catch (ArgumentOutOfRangeException)
            {
                var msg = String.Format("PRICE {0}", _avliableProducts[reqestedProduct].Price.ToString("C", CultureInfo.CurrentCulture));
                _dispManager.OnDisplayUpdate(new DisplayUpdateEventArgs { Message = msg });
                _coinManager.DisplayCurrentAmount();
                return null;
            }
        }
    }
}

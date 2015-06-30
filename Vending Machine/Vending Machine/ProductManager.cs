using System;
using System.Collections.Generic;
using VendingMachine.Products;

namespace VendingMachine
{
    public class ProductManager
    {
        private Dictionary<String, IProduct> _avliableProducts = new Dictionary<string, IProduct>
        {
            {"COLA", new Cola()}
        };

        public IProduct Buy(string product)
        {
            var reqestedProduct = product.ToUpperInvariant();
            if (reqestedProduct == "COLA")
                return _avliableProducts[reqestedProduct];
            throw new NotImplementedException();
        }
    }
}

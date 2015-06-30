using System.Globalization;

namespace VendingMachine.Products
{
    public abstract class ProductBase : IProduct
    {
        public virtual decimal Price
        {
            get { return _price; }
        }

        private readonly decimal _price;

        protected ProductBase(decimal price)
        {
            _price = price;
        }
    }
}

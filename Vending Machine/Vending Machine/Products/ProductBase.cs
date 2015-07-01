using System.Globalization;

namespace VendingMachine.Products
{
    public abstract class ProductBase : IProduct
    {
        public virtual decimal Price
        {
            get { return _price; }
        }

        public virtual uint Inventory { get; set; }
        public bool IsOutOfStock 
        {
            get { return Inventory == 0; }
        }

        private readonly decimal _price;

        protected ProductBase(decimal price)
        {
            _price = price;
        }
    }
}

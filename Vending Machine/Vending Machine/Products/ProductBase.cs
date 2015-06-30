using System.Globalization;

namespace VendingMachine.Products
{
    public abstract class ProductBase : IProduct
    {
        public virtual string Price
        {
            get { return _price.ToString("C", CultureInfo.CurrentCulture); }
        }

        protected decimal _price;

        protected ProductBase(decimal price)
        {
            _price = price;
        }
    }
}

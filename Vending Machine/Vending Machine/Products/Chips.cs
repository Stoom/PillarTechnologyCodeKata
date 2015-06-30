using System.Globalization;

namespace VendingMachine.Products
{
    public class Chips : IProduct
    {
        public string Price 
        {
            get { return _price.ToString("C", CultureInfo.CurrentCulture); }
        }

        private decimal _price = (decimal) 0.50;
    }
}

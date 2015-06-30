using System.Globalization;

namespace VendingMachine.Products
{
    public class Cola : IProduct
    {
        public string Price 
        {
            get { return _price.ToString("C", CultureInfo.CurrentCulture); }
        }

        private decimal _price = (decimal) 1.00;
    }
}

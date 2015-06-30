using System.Globalization;

namespace VendingMachine.Products
{
    public class Cola : ProductBase
    {
        private const decimal PRICE = (decimal) 1.00;

        public Cola() : base(PRICE)
        {
        }
    }
}

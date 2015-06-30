using System.Globalization;

namespace VendingMachine.Products
{
    public class Chips : ProductBase
    {
        private const decimal PRICE = (decimal) 0.50;

        public Chips() : base(PRICE)
        {
        }
    }
}

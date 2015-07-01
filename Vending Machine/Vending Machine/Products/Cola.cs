using System.Globalization;

namespace VendingMachine.Products
{
    public class Cola : ProductBase
    {
        private const decimal PRICE = (decimal) 1.00;
        private const uint INVENTORY = 2;

        public Cola() : base(PRICE)
        {
            Inventory = INVENTORY;
        }
    }
}

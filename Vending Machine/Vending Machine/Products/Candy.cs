namespace VendingMachine.Products
{
    public class Candy : ProductBase
    {
        private const decimal PRICE = (decimal)0.65;
        private const uint INVENTORY = 1;

        public Candy() : base(PRICE)
        {
            Inventory = INVENTORY;
        }
    }
}

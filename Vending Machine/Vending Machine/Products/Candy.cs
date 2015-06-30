namespace VendingMachine.Products
{
    public class Candy : ProductBase
    {
        private const decimal PRICE = (decimal) 0.65;

        public Candy() : base(PRICE)
        {
        }
    }
}

namespace VendingMachine.Products
{
    public class Cola : IProduct
    {
        public decimal Price 
        {
            get { return _price; }
        }

        private decimal _price = (decimal) 1.00;
    }
}

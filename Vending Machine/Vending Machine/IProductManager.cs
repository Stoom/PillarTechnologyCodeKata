using VendingMachine.Products;

namespace VendingMachine
{
    public interface IProductManager
    {
        IProduct Buy(string product);
    }
}

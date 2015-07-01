using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Products
{
    public interface IProduct
    {
        decimal Price { get; }
        uint Inventory { get; set; }
        bool IsOutOfStock { get; }
    }
}

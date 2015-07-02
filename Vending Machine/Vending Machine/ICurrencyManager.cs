using System.Collections.Generic;

namespace VendingMachine
{
    interface ICurrencyManager<T>
    {
        void DisplayCurrentAmount();
        void Insert(T currency);
        void Subtract(decimal price);
        Dictionary<T, int> GetChange();
        Dictionary<T, int> ReturnCoins();
    }
}

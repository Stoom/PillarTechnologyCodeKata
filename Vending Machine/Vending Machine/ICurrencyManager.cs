using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public interface ICurrencyManager
    {
        event EventHandler CurrencyDispensed;
        string CurrentAmount { get; }
        void DisplayCurrentAmount();
        void Subtract(decimal price);
    }

    public interface ICurrencyManager<T>
    {
        event EventHandler CurrencyDispensed;
        string CurrentAmount { get; }
        void DisplayCurrentAmount();
        void Subtract(decimal price);
        void Insert(T currency);
        Dictionary<T, int> GetChange();
        Dictionary<T, int> ReturnCurrency();
    }
}

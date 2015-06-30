using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public enum Coins
    {
        Penny = 1,
        Nickel = 5,
        Dime = 10,
        Quarter = 25,
        FiftyCent = 50,
        SilverDollar = 100,
        GoldDollar = 100
    }

    public static class CoinsExtensions
    {
        public static decimal ToDecimal(this Coins value)
        {
            return (decimal) value/100;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class CoinManager
    {
        private readonly HashSet<Coins> ACCEPTED_COINS = new HashSet<Coins>
            {
                Coins.Nickel,
                Coins.Dime,
                Coins.Quarter
            };

        public string CurrentAmount
        {
            get { return _currentAmount.ToString("C", CultureInfo.CurrentCulture); }
        }

        private decimal _currentAmount = (decimal)0.00;

        public void Insert(Coins coin)
        {
            if (ACCEPTED_COINS.Contains(coin))
                _currentAmount = coin.ToDecimal();
            else
                throw new ArgumentException("Invalid coin inserted");
        }
    }
}

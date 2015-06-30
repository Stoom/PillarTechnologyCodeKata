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
            get
            {
                return (_currentAmount > 0)
                            ? _currentAmount.ToString("C", CultureInfo.CurrentCulture)
                            : "INSERT COIN";
            }
        }

        private decimal _currentAmount = (decimal)0.00;
        private readonly DisplayManager _dispManager;

        public CoinManager(DisplayManager displayManager)
        {
            _dispManager = displayManager;
        }

        public void Insert(Coins coin)
        {
            if (ACCEPTED_COINS.Contains(coin))
            {
                _currentAmount += coin.ToDecimal();
                DisplayCurrentAmount();
            }
            else
            {
                throw new ArgumentException("Invalid coin inserted");
            }
        }

        public void Subtract(decimal price)
        {
            if (_currentAmount >= price)
            {
                _currentAmount -= price;
            }
            else
            {
                throw new ArgumentOutOfRangeException("");
            }
        }

        public void DisplayCurrentAmount()
        {
            _dispManager.OnDisplayUpdate(new DisplayUpdateEventArgs { Message = CurrentAmount });
        }

        public void ResetCurrentAmount()
        {
            _currentAmount = (decimal)0.00;
            _dispManager.OnDisplayUpdate(new DisplayUpdateEventArgs { Message = CurrentAmount });
        }
    }
}

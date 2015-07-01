using System;
using System.Collections.Generic;
using System.Globalization;

namespace VendingMachine
{
    public class CoinManager
    {
        public event EventHandler ChangeDispensed;

        // List in largest to smallest values
        private readonly List<Coins> ACCEPTED_COINS = new List<Coins>
        {
            Coins.Quarter,
            Coins.Dime,
            Coins.Nickel
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

                // Dispense change if there is anything left
                if (_currentAmount > 0)
                {
                    OnChangeDispensed();
                }
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

        public Dictionary<Coins, int> GetChange()
        {
            var changeReturned = new Dictionary<Coins, int>();

            foreach (var coin in ACCEPTED_COINS)
            {
                while (_currentAmount >= coin.ToDecimal())
                {
                    _currentAmount -= coin.ToDecimal();
                    if (!changeReturned.ContainsKey(coin))
                    {
                        changeReturned.Add(coin, 1);
                    }
                    else
                    {
                        changeReturned[coin]++;
                    }
                }
            }

            return changeReturned;
        }

        public Dictionary<Coins, int> ReturnCoins()
        {
            var changeReturned = GetChange();
            DisplayCurrentAmount();
            return changeReturned;
        }

        private void OnChangeDispensed()
        {
            var handler = ChangeDispensed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}

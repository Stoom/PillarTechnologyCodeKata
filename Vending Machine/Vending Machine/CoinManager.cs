using System;
using System.Collections.Generic;
using System.Globalization;

namespace VendingMachine
{
    public class CoinManager
    {
        public event EventHandler ChangeDispensed;

        public bool IsChangeDispensed { get; private set; }

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

                // Dispense change if there is anything left
                if (_currentAmount > 0)
                {
                    IsChangeDispensed = true;
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
            var change = _currentAmount;

            // Get quarters
            while (change >= Coins.Quarter.ToDecimal())
            {
                change -= Coins.Quarter.ToDecimal();

                if (!changeReturned.ContainsKey(Coins.Quarter))
                {
                    changeReturned.Add(Coins.Quarter, 1);
                }
                else
                {
                    changeReturned[Coins.Quarter]++;
                }
            }
            // Get dimes
            while (change >= Coins.Dime.ToDecimal())
            {
                change -= Coins.Dime.ToDecimal();

                if (!changeReturned.ContainsKey(Coins.Dime))
                {
                    changeReturned.Add(Coins.Dime, 1);
                }
                else
                {
                    changeReturned[Coins.Dime]++;
                }
            }
            // Get nickles
            while (change >= Coins.Nickel.ToDecimal())
            {
                change -= Coins.Nickel.ToDecimal();

                if (!changeReturned.ContainsKey(Coins.Nickel))
                {
                    changeReturned.Add(Coins.Nickel, 1);
                }
                else
                {
                    changeReturned[Coins.Nickel]++;
                }
            }

            return changeReturned;
        }

        public void ResetCurrentAmount()
        {
            _currentAmount = (decimal)0.00;
            _dispManager.OnDisplayUpdate(new DisplayUpdateEventArgs { Message = CurrentAmount });
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

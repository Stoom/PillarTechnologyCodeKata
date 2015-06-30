using System;

namespace VendingMachine
{
    public class DisplayManager
    {
        public event EventHandler<DisplayUpdateEventArgs> DisplayUpdate;

        internal void OnDisplayUpdate(DisplayUpdateEventArgs e)
        {
            var handler = DisplayUpdate;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}

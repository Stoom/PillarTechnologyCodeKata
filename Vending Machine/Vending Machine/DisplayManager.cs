using System;

namespace VendingMachine
{
    public class DisplayManager : IDisplayManager
    {
        public event EventHandler<DisplayUpdateEventArgs> DisplayUpdate;

        public void OnDisplayUpdate(DisplayUpdateEventArgs e)
        {
            var handler = DisplayUpdate;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}

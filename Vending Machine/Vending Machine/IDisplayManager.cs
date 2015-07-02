using System;

namespace VendingMachine
{
    public interface IDisplayManager
    {
        event EventHandler<DisplayUpdateEventArgs> DisplayUpdate;
        void OnDisplayUpdate(DisplayUpdateEventArgs e);
    }
}

using System;

namespace VendingMachine
{
    public class DisplayUpdateEventArgs : EventArgs
    {
        public string Message { get; set; }
        public TimeSpan Duration { get; set; }
    }
}

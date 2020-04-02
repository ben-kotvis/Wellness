using System;
using System.Collections.Generic;
using System.Text;

namespace Wellness.Model
{
    public class MonthChangedEventArgs : EventArgs
    {
        public Month Month { get; private set; }

        public MonthChangedEventArgs(Month month)
        {
            Month = month;
        }
    }
}

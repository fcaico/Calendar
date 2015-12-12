using System;

namespace Fcaico.Controls.Calendar
{
    public class DateSelectedEventArgs : EventArgs
    {
        public DateTime Date
        {
            get;
            set;
        }

        public DateSelectedEventArgs(DateTime date)
        {
            Date = date;
        }
    }

}


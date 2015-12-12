using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using CoreGraphics;

namespace CalendarSample
{
	partial class CalenderViewController : UIViewController
	{
		public DateTime SelectedDate 
		{
			get;
			set;
		}

		public CalenderViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            Calendar.DayNamesBackgroundColor = UIColor.Orange;
            Calendar.DayNamesColor = UIColor.Black;
			Calendar.Date = DateTime.Now;
			Calendar.SelectedDate = SelectedDate;
            Calendar.UseDayInitials = true;
			Calendar.HidePreviousAndNextMonthDays = true;
		}


	}
}

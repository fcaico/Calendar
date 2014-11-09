using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Drawing;

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

			Calendar.Date = DateTime.Now;
			Calendar.SelectedDate = SelectedDate;

			Calendar.UseDayInitials = true;
			Calendar.HidePreviousAndNextMonthDays = true;
		}
	}
}

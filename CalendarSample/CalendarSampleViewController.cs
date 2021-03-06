﻿using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace CalendarSample
{
	public partial class CalendarSampleViewController : UIViewController
	{
		public CalendarSampleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			TryDatePicker.TimeZone = NSTimeZone.LocalTimeZone;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			var calendarVC = segue.DestinationViewController as CalenderViewController;
			if (calendarVC != null) {
                calendarVC.SelectedDate = TryDatePicker.Date.NSDateToDateTime();
			}
		}
		#endregion
	}
}


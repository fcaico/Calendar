// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace CalendarSample
{
	[Register ("CalendarSampleViewController")]
	partial class CalendarSampleViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton TryDateButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIDatePicker TryDatePicker { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (TryDateButton != null) {
				TryDateButton.Dispose ();
				TryDateButton = null;
			}
			if (TryDatePicker != null) {
				TryDatePicker.Dispose ();
				TryDatePicker = null;
			}
		}
	}
}

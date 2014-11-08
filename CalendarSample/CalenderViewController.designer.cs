// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace CalendarSample
{
	[Register ("CalenderViewController")]
	partial class CalenderViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Fcaico.Controls.Calendar.CalendarView Calendar { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (Calendar != null) {
				Calendar.Dispose ();
				Calendar = null;
			}
		}
	}
}

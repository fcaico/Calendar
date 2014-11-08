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

			//Calendar.BackgroundColor = Themes.CrudFitEvent.ViewBackgroundColor;
			Calendar.Date = DateTime.Now;
			Calendar.SelectedDate = SelectedDate;

			Calendar.UseDayInitials = true;
			Calendar.HidePreviousAndNextMonthDays = true;
			//Calendar.DayNamesColor = Themes.Colors.YuWhite;
			//Calendar.DayNamesBackgroundColor = Themes.Colors.YuYellow;
			//Calendar.SelectionColor = Themes.Colors.YuYellow;
			//Calendar.MonthSeparatorColor = Themes.Colors.YuWhite;
			//Calendar.RuleColor = Themes.Colors.YuYellow;
			//Calendar.DayNameFont = Themes.CrudFitEvent.ActivityTimeView.DayNameFont;
			//Calendar.MonthFont = Themes.CrudFitEvent.ActivityTimeView.MonthFont;
			//Calendar.WeekDayFont = Themes.CrudFitEvent.ActivityTimeView.WeekDayFont;
			//Calendar.WeekendFont = Themes.CrudFitEvent.ActivityTimeView.WeekendFont;
			//Calendar.SelectionFont = Themes.CrudFitEvent.ActivityTimeView.SelectionFont;

			Calendar.PreviousMonthImage = Resources.ImageOfPreviousMonth(
				new RectangleF(0, 0, 30, 30), UIColor.Orange);

			Calendar.NextMonthImage = Resources.ImageOfNextMonth(
				new RectangleF(0, 0, 30, 30), UIColor.Orange);

			//var set = this.CreateBindingSet<SelectActivityTimeView, SelectActivityTimeViewModel>();

			//TimePicker.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 
			//	DateTime.Now.AddHours(1).Hour, 0, 0);

			//TimePicker.RuleColor = Themes.Colors.YuYellow;
			//TimePicker.LabelFont = Themes.CrudFitEvent.ActivityTimeView.TimeLabelFont;
			//TimePicker.TimeFont = Themes.CrudFitEvent.ActivityTimeView.TimeFont;
			//TimePicker.AmPmFont = Themes.CrudFitEvent.ActivityTimeView.AmPmFont;

			//TimePicker.BackImage = Themes.AnnotationStyleKit.ImageOfDownArrow(
			//	new RectangleF(0, 0, 30, 30), Themes.Colors.YuYellow);

			//TimePicker.ForwardImage = Themes.AnnotationStyleKit.ImageOfUpArrow(
			//	new RectangleF(0, 0, 30, 30), Themes.Colors.YuYellow);


			//set.Bind(Calendar).For(v => v.SelectedDate).To(vm => vm.SelectedDate);
			//set.Bind(Calendar).For(v => v.Date).To(vm => vm.Today);
			//set.Bind(TimePicker).For(v => v.Time).To(vm => vm.SelectedTime);
			//set.Bind(SaveIt).For("Title").To(vm => vm.SaveEventTimeCommand.DisplayName);
			//set.Bind(SaveIt).To(vm => vm.SaveEventTimeCommand.Command);
			//set.Apply();
		}
	}
}

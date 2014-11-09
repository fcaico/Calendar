using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace Fcaico.Controls.Calendar
{
	internal static class CalendarViewStyleKit
	{
		public static UIColor DayNamesColor = UIColor.Black;
		public static UIColor DayNamesBackgroundColor = UIColor.Clear;
		public static UIColor DaysBackgroundColor = UIColor.Clear;
		public static UIColor WeekdaysColor = UIColor.Black;
		public static UIColor WeekendDaysColor = UIColor.Black;
		public static UIColor PreviousAndNextMonthDaysColor = UIColor.LightGray;

		public static UIColor MonthColor = UIColor.Black;
		public static UIColor MonthBackgroundColor = UIColor.Clear;
		public static UIColor MonthSeparatorColor = UIColor.Clear;
		public static UIColor SelectionColor = UIColor.Orange;
		public static UIColor RuleColor = UIColor.White;

		public static UIFont DayNameFont = UIFont.SystemFontOfSize(22);
		public static UIFont MonthFont = UIFont.SystemFontOfSize(22);
		public static UIFont WeekDayFont = UIFont.SystemFontOfSize(22);
		public static UIFont WeekEndFont = UIFont.SystemFontOfSize(22);
		public static UIFont SelectionFont = UIFont.SystemFontOfSize(22);

		public static void DrawPreviousMonth(RectangleF frame, UIColor monthChangeColor)
		{
			// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(frame.GetMinX() + 19.93f, frame.GetMinY() + 2.11f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 7.31f, frame.GetMinY() + 14.77f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 7.3f, frame.GetMinY() + 13.34f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 20.03f, frame.GetMinY() + 25.65f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 18.64f, frame.GetMinY() + 27.09f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 5.91f, frame.GetMinY() + 14.78f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 5.18f, frame.GetMinY() + 14.08f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 5.9f, frame.GetMinY() + 13.36f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 18.51f, frame.GetMinY() + 0.7f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 19.93f, frame.GetMinY() + 2.11f));
			bezierPath.ClosePath();
			monthChangeColor.SetFill();
			bezierPath.Fill();
		}

		public static void DrawNextMonth(RectangleF frame, UIColor monthChangeColor)
		{
			// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(frame.GetMinX() + 7.21f, frame.GetMinY() + 3.22f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 19.82f, frame.GetMinY() + 15.88f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 19.83f, frame.GetMinY() + 14.45f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 7.1f, frame.GetMinY() + 26.75f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 8.49f, frame.GetMinY() + 28.19f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 21.22f, frame.GetMinY() + 15.89f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 21.95f, frame.GetMinY() + 15.18f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 21.24f, frame.GetMinY() + 14.46f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 8.62f, frame.GetMinY() + 1.81f));
			bezierPath.AddLineTo(new PointF(frame.GetMinX() + 7.21f, frame.GetMinY() + 3.22f));
			bezierPath.ClosePath();
			monthChangeColor.SetFill();
			bezierPath.Fill();
		}

		public static UIImage ImageOfPreviousMonth(RectangleF frame, UIColor monthChangeColor)
		{
			UIGraphics.BeginImageContextWithOptions(frame.Size, false, 0);
			CalendarViewStyleKit.DrawPreviousMonth(frame, monthChangeColor);

			var imageOfPreviousMonth = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			return imageOfPreviousMonth;
		}

		public static UIImage ImageOfNextMonth(RectangleF frame, UIColor monthChangeColor)
		{
			UIGraphics.BeginImageContextWithOptions(frame.Size, false, 0);
			CalendarViewStyleKit.DrawNextMonth(frame, monthChangeColor);

			var imageOfNextMonth = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			return imageOfNextMonth;
		}
	}
}


using System;
using System.Drawing;
using MonoTouch.UIKit;
using System.Globalization;
using MonoTouch.Foundation;

namespace Fcaico.Controls.Calendar
{
    internal class DayNameView : UILabel
    {
        private static readonly string[] _daysOfWeek;
        private readonly CalendarView _calendar;
        private DayOfWeek _dayOfWeek = DateTime.Now.DayOfWeek;

        public DayOfWeek DayName
        {
            get
            {
                return _dayOfWeek;
            }
            set
            {
                _dayOfWeek = value;
                SetNeedsDisplay();
            }
        }

        static DayNameView() 
        {
            var cultureInfo = new CultureInfo(NSLocale.CurrentLocale.LanguageCode);           
            _daysOfWeek =  cultureInfo.DateTimeFormat.AbbreviatedDayNames;
        }

        public DayNameView (CalendarView parent) : base ()
        {
            _calendar = parent;
            AdjustsFontSizeToFitWidth = true;
        }

        public override void Draw (RectangleF rect)
        {
            Text = _daysOfWeek[(int) _dayOfWeek];
            if (_calendar.UseDayInitials)
            {
                Text = Text.Substring(0, 1);
            }
            TextColor = _calendar.DayNamesColor;
            TextAlignment = UITextAlignment.Center;
            Font = _calendar.DayNameFont;
           
            base.Draw(rect);
        }
    }
}


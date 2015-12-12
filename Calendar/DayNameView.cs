using System;
using CoreGraphics;
using UIKit;
using System.Globalization;
using Foundation;

namespace Fcaico.Controls.Calendar
{
    internal class DayNameView : UIView
    {
        private static readonly string[] _daysOfWeek;
        private readonly CalendarView _calendar;
        private DayOfWeek _dayOfWeek = DateTime.Now.DayOfWeek;
        private readonly UILabel _textView;
        NSLayoutConstraint _textBaselineConstraint;

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
            BackgroundColor = UIColor.Clear;

            _textView = new UILabel();
            _textView.AdjustsFontSizeToFitWidth = true;
            _textView.TextAlignment = UITextAlignment.Center;
            Add(_textView);

            SetupConstraints();
        }


        private void SetupConstraints()
        {
            _textView.TranslatesAutoresizingMaskIntoConstraints = false;
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _textView, NSLayoutAttribute.CenterX, 1, 0));

            _textBaselineConstraint = NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _textView, NSLayoutAttribute.CenterY, 1, -_calendar.DayNameFontBaselineOffset);
            AddConstraint(_textBaselineConstraint);

        }

        private void SetFont()
        {
            _textView.Font = _calendar.DayNameFont;
        }

        private void SetTextColor()
        {
            _textView.TextColor = _calendar.DayNamesColor;
        }

        private void SetText()
        {            
            string text = _daysOfWeek[(int) _dayOfWeek];

            _textView.Text = _calendar.UseDayInitials 
                ? text.Substring(0, 1)
                : text;

            _textView.SizeToFit();  
        }

        public override void Draw (CGRect rect)
        {          
            base.Draw(rect);

            _textBaselineConstraint.Constant = -_calendar.DayNameFontBaselineOffset;
            SetNeedsUpdateConstraints();
           
            SetText();
            SetTextColor();
            SetFont();

         }
    }
}


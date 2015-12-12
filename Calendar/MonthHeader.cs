using System;
using UIKit;
using CoreGraphics;

namespace Fcaico.Controls.Calendar
{
	internal class MonthHeader : UIView 
	{
		private readonly UILabel _monthLabel;
        private readonly UIButton _previousButton;
        private readonly UIButton _nextButton;
        private readonly UIView _separator;
        private readonly CalendarView _calendar;
        NSLayoutConstraint _monthlabelBaselineConstraint;

        private DateTime _date = DateTime.Now;

        public EventHandler PreviousMonth;
        public EventHandler NextMonth;

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                SetNeedsDisplay();
            }
        }

        public string MonthName
        {
            get
            {
                return Date.ToString(_calendar.MonthFormatString);
            }
        }

		public MonthHeader(CalendarView parent) : base ()
		{
            _calendar = parent;
            BackgroundColor = _calendar.MonthBackgroundColor;

            _monthLabel = new UILabel();
            _monthLabel.TextAlignment = UITextAlignment.Center;

            _previousButton = new UIButton();
            _previousButton.SizeToFit();
            _previousButton.TouchUpInside += RaisePreviousMonth;

            _nextButton = new UIButton();
            _nextButton.SizeToFit();
            _nextButton.TouchUpInside += RaiseNextMonth;

            _separator = new UIView();

            Add(_previousButton);
            Add(_monthLabel);
            Add(_nextButton);
            Add(_separator);

            SetupConstraints();
		}

        private void SetupConstraints()
        {
            _monthLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            _previousButton.TranslatesAutoresizingMaskIntoConstraints = false;
            _nextButton.TranslatesAutoresizingMaskIntoConstraints = false;
            _separator.TranslatesAutoresizingMaskIntoConstraints = false;

            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _monthLabel, NSLayoutAttribute.CenterX, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_monthLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 0.4f, 0));

            _monthlabelBaselineConstraint = NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _monthLabel, NSLayoutAttribute.CenterY, 1, 
                -_calendar.MonthFontBaselineOffset);
            AddConstraint(_monthlabelBaselineConstraint);


            AddConstraint(NSLayoutConstraint.Create(_monthLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _previousButton, NSLayoutAttribute.Right, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _previousButton, NSLayoutAttribute.CenterY, 1, 0));

            AddConstraint(NSLayoutConstraint.Create(_monthLabel, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _nextButton, NSLayoutAttribute.Left, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _nextButton, NSLayoutAttribute.CenterY, 1, 0));           

            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _separator, NSLayoutAttribute.CenterX, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _separator, NSLayoutAttribute.Bottom, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_separator, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 0.8f, 0));
            AddConstraint(NSLayoutConstraint.Create(_separator, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0, 2));

        }

        #region IDisposable/Memory management stuff

        protected override void Dispose (bool disposing)
        {
            if (disposing)
            {
                _previousButton.TouchUpInside -= RaisePreviousMonth;
                _nextButton.TouchUpInside -= RaiseNextMonth;
            }

            base.Dispose(disposing);
        }
        #endregion

        void RaisePreviousMonth (object sender, EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler  handler = PreviousMonth;

            // Event will be null if there are no subscribers 
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, new EventArgs());
            }
        }

        void RaiseNextMonth (object sender, EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler  handler = NextMonth;

            // Event will be null if there are no subscribers 
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, new EventArgs());
            }
        }

        public override void Draw (CGRect rect)
        {
            base.Draw(rect);

            BackgroundColor = _calendar.MonthBackgroundColor;

            _monthlabelBaselineConstraint.Constant = -_calendar.MonthFontBaselineOffset;
            SetNeedsUpdateConstraints();

            _monthLabel.TextColor = _calendar.MonthColor;
            _monthLabel.Text = MonthName;
            _monthLabel.Font = _calendar.MonthFont;

            if (_calendar.DisablePastDates)
            {
                _previousButton.Hidden = _calendar.IsInTheCurrentMonth(DateTime.Now);
            }

            _previousButton.SetImage(_calendar.PreviousMonthImage, UIControlState.Normal);
            _nextButton.SetImage(_calendar.NextMonthImage, UIControlState.Normal);

            _separator.BackgroundColor = _calendar.MonthSeparatorColor;
        }


	}
}


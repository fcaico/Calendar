using System;
using UIKit;
using CoreGraphics;

namespace Fcaico.Controls.Calendar
{
    internal sealed class DayView : UIView
    {
        #region Data Members;

        private readonly CalendarView _calendar;
        private DateTime _dayRepresented = DateTime.Now;
        private bool _selected;
        private bool _isCurrentMonth;
        private bool _isInThePast;
        private readonly UIImageView _imageView;
        private readonly UILabel _textView;
        NSLayoutConstraint _textBaselineConstraint;

        #endregion 

        public event EventHandler DaySelected;  

        #region Properties

        public DateTime DayRepresented
        {
            get
            {
                return _dayRepresented;
            }
            set
            {
                if (_dayRepresented != value)
                {
                    _dayRepresented = value;
                    SetNeedsDisplay();
                }
            }
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    SetNeedsDisplay();
                }
            }
        }

        public bool IsInThePast
        {
            get
            {
                return _isInThePast;
            }
            set
            {
                if (_isInThePast != value)
                {
                    _isInThePast = value;
                    SetNeedsDisplay();
                }
            }
        }

        public bool IsCurrentMonth
        {
            get
            {
                return _isCurrentMonth;
            }
            set
            {
                if (_isCurrentMonth != value)
                {
                    _isCurrentMonth = value;
                    SetNeedsDisplay();
                }
            }
        }

        private bool IsWeekend
        {
            get
            {
                return (
                    _dayRepresented.DayOfWeek == DayOfWeek.Saturday ||
                    _dayRepresented.DayOfWeek == DayOfWeek.Sunday);
            }
        }

        private bool IsWeekday
        {
            get
            {
                return !IsWeekend;
            }
        }


        #endregion

        #region Construction Destruction

        public DayView (CalendarView calendar) : base ()
        {
            _calendar = calendar;
            BackgroundColor = UIColor.Clear;

            _textView = new UILabel();
            _textView.AdjustsFontSizeToFitWidth = true;
            _textView.TextAlignment = UITextAlignment.Center;
            Add(_textView);

            _imageView = new UIImageView(Bounds);
            Add(_imageView);

            SetupConstraints();
        }

        private void SetupConstraints()
        {
            _textView.TranslatesAutoresizingMaskIntoConstraints = false;
            _imageView.TranslatesAutoresizingMaskIntoConstraints = false;

            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _textView, NSLayoutAttribute.CenterX, 1, 0));

            _textBaselineConstraint = NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _textView, NSLayoutAttribute.CenterY, 1, -_calendar.DayFontBaselineOffset);
            AddConstraint(_textBaselineConstraint);

            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.Width, NSLayoutRelation.Equal, _imageView, NSLayoutAttribute.Width, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.Height, NSLayoutRelation.Equal, _imageView, NSLayoutAttribute.Height, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _imageView, NSLayoutAttribute.CenterX, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _imageView, NSLayoutAttribute.CenterY, 1, 0));           
        }

        #endregion

        #region Methods

        private void RaiseDaySelected()
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler handler = DaySelected;

            // Event will be null if there are no subscribers 
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, new EventArgs());
            }
        }

        private void SelectDateIfAppropriate()
        {
            if (IsInThePast && _calendar.DisablePastDates)
            {
                return;
            }

            if (IsCurrentMonth && !Selected)
            {
                Selected = true;
                RaiseDaySelected();
            }
        }

        public override void TouchesBegan (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan (touches, evt);
            SelectDateIfAppropriate();
        }

        public override void TouchesMoved (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            SelectDateIfAppropriate();
        }

        public override void TouchesEnded (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            SelectDateIfAppropriate();
        }

        private void SetFont()
        {
            if (Selected)
            {
                _textView.Font = _calendar.SelectionFont;
            }
            else
            {   
                _textView.Font = IsWeekday ? _calendar.WeekDayFont : _calendar.WeekendFont;
            }
        }

        private void SetTextColor()
        {
            if (_selected)
            {
                _textView.TextColor = _calendar.SelectionColor;
            }
            else if ((_calendar.DisablePastDates && IsInThePast) || !IsCurrentMonth)
            {
                _textView.TextColor = _calendar.PreviousAndNextMonthDaysColor;
            }
            else if (IsWeekend)
            {
                _textView.TextColor = _calendar.WeekendDaysColor;
            }
            else
            {
                _textView.TextColor = _calendar.WeekDaysColor;
            }
        }

        private void SetImage()
        {
            if (_calendar.SelectionImage != null)
            {
                if (_selected)
                {
                    _imageView.Image = _calendar.SelectionImage;
                }
                else
                {
                    _imageView.Image = null;
                }
            }
        }

        private void SetText()
        {
            if (_calendar.HidePreviousAndNextMonthDays &&
                !IsCurrentMonth)
            {
                _textView.Text = string.Empty;
                _textView.SizeToFit();
            }
            else
            {
                _textView.Text = _dayRepresented.Day.ToString();
                _textView.SizeToFit();
            }
        }

        public override void Draw (CGRect rect)
        {
            base.Draw(rect);

            _textBaselineConstraint.Constant = -_calendar.DayFontBaselineOffset;
            SetNeedsUpdateConstraints();

            if (Selected)
            {
                Console.WriteLine("Drawing a selected day for {0} ", _dayRepresented);
            }

            SetText();
            SetTextColor();
            SetFont();
            SetImage();
        }

        #endregion
    }
}


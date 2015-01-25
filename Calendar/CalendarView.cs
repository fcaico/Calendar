using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Drawing;
using System.ComponentModel;

namespace Fcaico.Controls.Calendar
{
	[Register ("CalendarView"), DesignTimeVisible(true)]
	public class CalendarView : UIView, IComponent
	{
        #region Data Members

        private DateTime _date = DateTime.Now;
        private DateTime _selectedDate = DateTime.Now;

        private MonthHeader _monthHeader;
        private DayGrid _dayGrid;
        private bool _disablePastDates = true;
        private bool _sundayFirst = true;
        private bool _useDayInitials = false;
        private bool _hidePreviousAndNextMonthDays = true;
        private string _monthFormatString = "MMMM";

		private UIColor _dayNamesColor = CalendarViewStyleKit.DayNamesColor;
		private UIColor _dayNamesBackgroundColor = CalendarViewStyleKit.DayNamesBackgroundColor;
		private UIColor _daysBackgroundColor = CalendarViewStyleKit.DaysBackgroundColor;
		private UIColor _weekdaysColor = CalendarViewStyleKit.WeekdaysColor;
		private UIColor _weekendDaysColor = CalendarViewStyleKit.WeekendDaysColor;
		private UIColor _previousAndNextMonthDaysColor = CalendarViewStyleKit.PreviousAndNextMonthDaysColor;

		private UIColor _monthColor = CalendarViewStyleKit.MonthColor;
		private UIColor _monthBackgroundColor = CalendarViewStyleKit.MonthBackgroundColor;
		private UIColor _monthSeparatorColor = CalendarViewStyleKit.MonthSeparatorColor;
		private UIColor _selectionColor = CalendarViewStyleKit.SelectionColor;
		private UIColor _ruleColor = CalendarViewStyleKit.RuleColor;

		private UIFont _dayNameFont = CalendarViewStyleKit.DayNameFont;
		private UIFont _monthFont = CalendarViewStyleKit.MonthFont;
		private UIFont _weekDayFont = CalendarViewStyleKit.WeekDayFont;
		private UIFont _weekEndFont = CalendarViewStyleKit.WeekEndFont;
		private UIFont _selectionFont = CalendarViewStyleKit.SelectionFont;

        private UIImage _previousMonthImage;
        private UIImage _nextMonthImage;
        private UIImage _selectionImage = null;

        private UIView _bottomRule = new UIView();

        #endregion 

        public event EventHandler SelectedDateChanged;

        #region Properties

        #region Look and Feel customizations

		[Export("DisablePastDates"), Browsable(true)]
		public bool DisablePastDates
		{
			get 
			{
				return _disablePastDates;
			}
            set
            {
                if (_disablePastDates != value)
                {
                    _disablePastDates = value;
                    SetNeedsDisplay();
                }
            }
		}

        [Export("SundayFirst"), Browsable(true)]
        public bool SundayFirst
        { 
            get
            {
                return _sundayFirst; 
            }
            set
            {
                if (_sundayFirst != value)
                {
                    _sundayFirst = value; 
                    SetNeedsDisplay();
                }
            }
        }

        [Export("UseDayInitials"), Browsable(true)]
        public bool UseDayInitials 
        { 
            get
            {
                return _useDayInitials;
            }
            set
            {
                if (_useDayInitials != value)
                {
                    _useDayInitials = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("HidePreviousAndNextMonthDays"), Browsable(true)]
        public bool HidePreviousAndNextMonthDays
        {
            get
            {
                return _hidePreviousAndNextMonthDays;
            }
            set
            {
                if (_hidePreviousAndNextMonthDays != value)
                {
                    _hidePreviousAndNextMonthDays = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("MonthFormatString"), Browsable(true)]
        public string MonthFormatString 
        { 
            get
            {
                return _monthFormatString;
            }
            set
            {
                if (_monthFormatString != value)
                {
                    _monthFormatString = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("DayNamesColor"), Browsable(true)]
        public UIColor DayNamesColor
        {
            get
            {
                return _dayNamesColor;
            }
            set
            {
                if (_dayNamesColor != value)
                {
                    _dayNamesColor = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("DayNamesBackgroundColor"), Browsable(true)]
        public UIColor DayNamesBackgroundColor
        {
            get
            {
                return _dayNamesBackgroundColor;
            }
            set
            {
                if (_dayNamesBackgroundColor != value)
                {
                    _dayNamesBackgroundColor = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("WeekDaysColor"), Browsable(true)]
        public UIColor WeekDaysColor
        {
            get
            {
                return _weekdaysColor;
            }
            set
            {
                if (_weekdaysColor != value)
                {
                    _weekdaysColor = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("DaysBackgroundColor"), Browsable(true)]
        public UIColor DaysBackgroundColor
        {
            get
            {
                return _daysBackgroundColor;
            }
            set
            {
                if (_daysBackgroundColor != value)
                {
                    _daysBackgroundColor = value;
                    SetNeedsDisplay();
                }
            }
        }
           
        [Export("WeekendDaysColor"), Browsable(true)]
        public UIColor WeekendDaysColor
        {
            get
            {
                return _weekendDaysColor;
            }
            set
            {
                if (_weekendDaysColor != value)
                {
                    _weekendDaysColor = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("PreviousAndNextMonthDaysColor"), Browsable(true)]
        public UIColor PreviousAndNextMonthDaysColor
        {
            get
            {
                return _previousAndNextMonthDaysColor;
            }
            set
            {
                if (_previousAndNextMonthDaysColor != value)
                {
                    _previousAndNextMonthDaysColor = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("MonthColor"), Browsable(true)]
        public UIColor MonthColor
        {
            get
            {
                return _monthColor;
            }
            set
            {
                if (_monthColor != value)
                {
                    _monthColor = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("MonthBackgroundColor"), Browsable(true)]
        public UIColor MonthBackgroundColor
        {
            get
            {
                return _monthBackgroundColor;
            }
            set
            {
                if (_monthBackgroundColor != value)
                {
                    _monthBackgroundColor = value;
                    SetNeedsDisplay();
                }
            }
        }



        [Export("MonthSeparatorColor"), Browsable(true)]
        public UIColor MonthSeparatorColor
        {
            get
            {
                return _monthSeparatorColor;
            }
            set
            {
                if (_monthSeparatorColor != value)
                {
                    _monthSeparatorColor = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("SelectionColor"), Browsable(true)]
        public UIColor SelectionColor
        { 
            get
            {
                return _selectionColor;
            }
            set
            {
                if (_selectionColor != value)
                {
                    _selectionColor = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("RuleColor"), Browsable(true)]
        public UIColor RuleColor
        {
            get
            {
                return _ruleColor;
            }
            set
            {
                if (_ruleColor != value)
                {
                    _ruleColor = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("PreviousMonthImage"), Browsable(true)]
        public UIImage PreviousMonthImage 
        {
            get
            {
                return _previousMonthImage;
            }
            set
            {
                if (_previousMonthImage != value)
                {
                    _previousMonthImage = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("NextMonthImage"), Browsable(true)]
        public UIImage NextMonthImage
        { 
            get
            {
                return _nextMonthImage;
            }
            set
            {
                if (_nextMonthImage != value)
                {
                    _nextMonthImage = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("SelectionImage"), Browsable(true)]
        public UIImage SelectionImage
        { 
            get
            {
                return _selectionImage;
            }
            set
            {
                if (_selectionImage != value)
                {
                    _selectionImage = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("MonthFont"), Browsable(true)]
        public UIFont MonthFont
        { 
            get
            {
                return _monthFont;
            }
            set
            {
                if (_monthFont != value)
                {
                    _monthFont = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("DayNameFont"), Browsable(true)]
        public UIFont DayNameFont 
        {
            get
            {
                return _dayNameFont;
            }
            set
            {
                if (_dayNameFont != value)
                {
                    _dayNameFont = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("WeekDayFont"), Browsable(true)]
        public UIFont WeekDayFont 
        {
            get
            {
                return _weekDayFont;
            }
            set
            {
                if (_weekDayFont != value)
                {
                    _weekDayFont = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("WeekendFont"), Browsable(true)]
        public UIFont WeekendFont
        {
            get
            {
                return _weekEndFont;
            }
            set
            {
                if (_weekEndFont != value)
                {
                    _weekEndFont = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("SelectionFont"), Browsable(true)]
        public UIFont SelectionFont
        {
            get
            {
                return _selectionFont;
            }
            set
            {
                if (_selectionFont != value)
                {
                    _selectionFont = value;
                    SetNeedsDisplay();
                }
            }
        }

        #endregion

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    _date = value;

                    if (_monthHeader != null)
                    {
                        _monthHeader.Date = _date;
                    }

                    if (_dayGrid != null)
                    {
                        _dayGrid.Date = _date;
                    }
                    SetNeedsDisplay();
                }
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;

                    if (_dayGrid != null)
                    {
                        _dayGrid.SelectedDate = value;
                    }
                    SetNeedsDisplay();
                }
            }
        }


        public ISite Site
        {
            get;
            set;
        }

        #endregion

        #region Construction and Destruction

		public CalendarView (IntPtr handle) : base (handle)
		{
		}


		public CalendarView () : base ()
		{
			Initialize ();
		}

		private void Initialize()
		{
            BackgroundColor = UIColor.Clear;
			SetupSubviews ();
			SetupConstraints ();
        }

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			Initialize ();
		}        

		protected override void Dispose (bool disposing)
        {
            if (disposing)
            {
                _monthHeader.NextMonth -= OnNextMonth;
                _monthHeader.PreviousMonth -= OnPreviousMonth;
                _dayGrid.DateSelected -= OnDateSelected;
            }
            base.Dispose(disposing);
        }

		private void SetupSubviews()
		{
			_monthHeader = new MonthHeader(this);
			_monthHeader.NextMonth += OnNextMonth;
			_monthHeader.PreviousMonth += OnPreviousMonth;

			_dayGrid = new DayGrid(this);
			_dayGrid.DateSelected += OnDateSelected;

			_previousMonthImage = CalendarViewStyleKit.ImageOfPreviousMonth(
				new RectangleF(0, 0, 30, 30), _selectionColor);

			_nextMonthImage = CalendarViewStyleKit.ImageOfNextMonth(
				new RectangleF(0, 0, 30, 30), _selectionColor);

			Add (_monthHeader);
			Add(_dayGrid);
			Add(_bottomRule);
		}

        private void SetupConstraints()
        {
            _monthHeader.TranslatesAutoresizingMaskIntoConstraints = false;
            _dayGrid.TranslatesAutoresizingMaskIntoConstraints = false;
            _bottomRule.TranslatesAutoresizingMaskIntoConstraints = false;

            AddConstraint(NSLayoutConstraint.Create(_monthHeader, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_monthHeader, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterX, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_monthHeader, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_monthHeader, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this, NSLayoutAttribute.Height, .15f, 0));


            AddConstraint(NSLayoutConstraint.Create(_dayGrid, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _monthHeader, NSLayoutAttribute.Bottom, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_dayGrid, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterX, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_dayGrid, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_dayGrid, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this, NSLayoutAttribute.Height, .85f, -1));

            AddConstraint(NSLayoutConstraint.Create(_bottomRule, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this, NSLayoutAttribute.Bottom, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_bottomRule, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterX, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_bottomRule, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(_bottomRule, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 1));

        }

		public event EventHandler Disposed;

		#endregion

        #region Methods

        public bool IsInTheCurrentMonth(DateTime day)
        {
            return (day.Month == Date.Month && day.Year == Date.Year);
        }

		public override void Draw (RectangleF rect)
		{
			_bottomRule.BackgroundColor = RuleColor;

			base.Draw(rect);
		}

		private void OnDateSelected (object sender, DateSelectedEventArgs e)
        {
            SelectedDate = e.Date;
            RaiseSelectedDateChanged();
        }

        private void OnNextMonth (object sender, EventArgs e)
        {
            Date = Date.AddMonths(1);
        }

        private void OnPreviousMonth (object sender, EventArgs e)
        {

            Date = Date.AddMonths(-1);
        }

        private void RaiseSelectedDateChanged()
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler  handler = SelectedDateChanged;

            // Event will be null if there are no subscribers 
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, new EventArgs());
            }
        }

        #endregion
	}
}

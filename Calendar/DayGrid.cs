using System;
using UIKit;
using CoreGraphics;
using System.Globalization;
using Foundation;
using System.Collections.Generic;

namespace Fcaico.Controls.Calendar
{
	internal class DayGrid : UIView
    {
        #region Data Members;

        private CalendarView _calendar;
        private DateTime _date = DateTime.Today;
        private DateTime _selectedDate = DateTime.Today;
        private DayView _selectedDay;

        private DayNameView[] _dayNameRow = new DayNameView[7];
        private DayView[,] _dayGrid = new DayView[6, 7];

        private UIView _container;
        private UIView _dayNamesContainer;
        private UIView _daysContainer;

        #endregion

        #region DateSelected event 
        public event EventHandler<DateSelectedEventArgs> DateSelected;  
      
        #endregion

        #region Properties

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

                    SetNeedsDisplay();
                }
            }
        }

        #endregion

        #region Construction / Destruction

        public DayGrid (CalendarView calendar) : base ()
        {
            _calendar = calendar;

            _container = new UIView();
            _container.ClipsToBounds = true;
            Add(_container);

            _dayNamesContainer = CreateDayNamesContainer();
            _container.Add(_dayNamesContainer);

            _daysContainer = CreateDaysContainer();
            _container.Add(_daysContainer);

            BackgroundColor = UIColor.Clear;

            SetupConstraints();
        }

        private UIView CreateDayNamesContainer()
        {
            UIView dayNamesContainer = new UIView();
            dayNamesContainer.ClipsToBounds = true;

            for (int i = 0; i < 7; i++)
            {
                _dayNameRow[i] = new DayNameView(_calendar);
                dayNamesContainer.Add(_dayNameRow[i]);
            }

            return dayNamesContainer;
        }


        private UIView CreateDaysContainer()
        {
            UIView daysContainer = new UIView();
            daysContainer.ClipsToBounds = true;

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    _dayGrid[row, col] = new DayView(_calendar);
                    _dayGrid[row, col].DaySelected += OnDayViewSelected;
                    daysContainer.Add(_dayGrid[row, col]);
                }
            }
            return daysContainer;
        }

        protected override void Dispose (bool disposing)
        {
            if (disposing)
            {
                for (int row = 0; row < 6; row++)
                {
                    for (int col = 0; col < 7; col++)
                    {
                        DayView day = _dayGrid[row, col];
                        if (day != null)
                        {
                            day.DaySelected -= OnDayViewSelected;
                        }
                    }
                }
            }
            base.Dispose(disposing);
        }

        private void SetupConstraints()
        {
            _container.TranslatesAutoresizingMaskIntoConstraints = false;
            _daysContainer.TranslatesAutoresizingMaskIntoConstraints = false;
            _dayNamesContainer.TranslatesAutoresizingMaskIntoConstraints = false;

            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _container, NSLayoutAttribute.CenterX, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _container, NSLayoutAttribute.CenterY, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.Width, NSLayoutRelation.Equal, _container, NSLayoutAttribute.Width, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.Height, NSLayoutRelation.Equal, _container, NSLayoutAttribute.Height, 1, 0));

            AddConstraint(NSLayoutConstraint.Create(_dayNamesContainer, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this, NSLayoutAttribute.Height, .15f, 0)); 
            AddConstraint(NSLayoutConstraint.Create(_daysContainer, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this, NSLayoutAttribute.Height, .85f, 0)); 

            _container.AddConstraint(NSLayoutConstraint.Create(_container, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _dayNamesContainer, NSLayoutAttribute.Top, 1, 0));
            _container.AddConstraint(NSLayoutConstraint.Create(_container, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _dayNamesContainer, NSLayoutAttribute.CenterX, 1, 0));
            _container.AddConstraint(NSLayoutConstraint.Create(_dayNamesContainer, NSLayoutAttribute.Width, NSLayoutRelation.Equal, _container, NSLayoutAttribute.Width, 1, 0)); 

            _container.AddConstraint(NSLayoutConstraint.Create(_dayNamesContainer, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _daysContainer, NSLayoutAttribute.Top, 1, 0));
            _container.AddConstraint(NSLayoutConstraint.Create(_container, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _daysContainer, NSLayoutAttribute.CenterX, 1, 0));
            _container.AddConstraint(NSLayoutConstraint.Create(_daysContainer, NSLayoutAttribute.Width, NSLayoutRelation.Equal, _container, NSLayoutAttribute.Width, 1, 0)); 
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the column a particular day is on.  (0 based)
        /// </summary>
        /// <returns>The column index.</returns>
        /// <param name="day">The day</param>
        private int ColumnIndexForDay(DayOfWeek day)
        {
            int index = (int) day;

            if (!_calendar.SundayFirst)
            {
                index = (day == DayOfWeek.Sunday) ? 6 : index - 1;
            }
            return index;
        }


        private DayOfWeek DayForColumnIndex(int i)
        {
            DayOfWeek day = (DayOfWeek)i;
            if (!_calendar.SundayFirst)
            {
                day = (i == 6) ? DayOfWeek.Sunday : (DayOfWeek) (i + 1);
            }
            return day;
        }

       

        private DayOfWeek MonthStartsOn(int year, int month)
        {
            return new DateTime(year, month, 1).DayOfWeek;
        }



        private void DrawDayNames()
        {
            CGPoint curLocation = new CGPoint(0, 0);
            CGSize cellSize = new CGSize(_dayNamesContainer.Bounds.Width / 7, _dayNamesContainer.Bounds.Height);

            for (int col = 0; col < 7; col++)
            {
                DayNameView dayNameCell = _dayNameRow[col];
                dayNameCell.DayName = DayForColumnIndex(col);
                dayNameCell.Frame = new CGRect(curLocation, cellSize);

                curLocation = new CGPoint(curLocation.X + cellSize.Width, curLocation.Y);
            }         
        }

        private bool IsInTheNextMonth(DateTime day)
        {
            DateTime nextMonth = _date.AddMonths(1);
            return (day.Month == nextMonth.Month &&
                    day.Year == nextMonth.Year);
        }
                


        private bool IsInThePast(DateTime day)
        {
            return (day < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
        }

        private void DrawDaysOfMonth()
        {
            CGPoint curLocation = new CGPoint(0, 0);
            CGSize cellSize = new CGSize(_daysContainer.Bounds.Width / 7, _daysContainer.Bounds.Height / 6);

            DateTime curDay = new DateTime(_date.Year, _date.Month, 1);

            int firstColumnOfThisfMonth = ColumnIndexForDay(MonthStartsOn(_date.Year, _date.Month));
            curDay = curDay.AddDays(- firstColumnOfThisfMonth);

            for (int row = 0; row < 6; row++)
            {
                if (IsInTheNextMonth(curDay))
                {
                    // if we are starting a new row and the next day is already outside of the current 
                    // month, then just throw out the whole row.

//                    _daysContainer.Frame = new RectangleF
//                    {
//                        Location = _daysContainer.Frame.Location,
//                        Size = new SizeF(_daysContainer.Frame.Width, _daysContainer.Frame.Height - _cellSize.Height)
//                    };

                    for (int col = 0; col < 7; col++)
                    {
                        DayView dayCell = _dayGrid[row, col];
                        dayCell.Hidden = true;
                        dayCell.IsCurrentMonth = false;
                    }
                }
                else
                {
                    for (int col = 0; col < 7; col++)
                    {
                        DayView dayCell = _dayGrid[row, col];
                        dayCell.Frame = new CGRect(curLocation, cellSize);
                        dayCell.Hidden = false;

						if (SelectedDate.IsSameDate(curDay))
                        {
                            dayCell.Selected = true;
                        }
                        else
                        {
                            dayCell.Selected = false;
                        }

                        dayCell.DayRepresented = curDay;
                        dayCell.IsCurrentMonth = _calendar.IsInTheCurrentMonth(curDay);
                        dayCell.IsInThePast = IsInThePast(curDay);


                        dayCell.SetNeedsDisplay();

                        curDay = curDay.AddDays(1);
                        curLocation = new CGPoint(curLocation.X + cellSize.Width, curLocation.Y);
                    }
                    curLocation = new CGPoint(0, curLocation.Y + cellSize.Height);
                }
            }
        }

       


        public override void Draw (CGRect rect)
        {
            base.Draw(rect);

            _dayNamesContainer.BackgroundColor = _calendar.DayNamesBackgroundColor;

            DrawDayNames();
            DrawDaysOfMonth();
        }
            
        private void OnDayViewSelected(object sender, EventArgs e)
        {
            DayView day = sender as DayView;
            if (day != null)
            {
                if (_selectedDay != null)
                {
                    _selectedDay.Selected = false;
                }
                _selectedDay = day;
                RaiseDateSelected(_selectedDay.DayRepresented);
            }
        }

        private void RaiseDateSelected(DateTime selectedDate)
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler<DateSelectedEventArgs>  handler = DateSelected;

            // Event will be null if there are no subscribers 
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, new DateSelectedEventArgs(selectedDate));
            }
        }

        #endregion
    }
}


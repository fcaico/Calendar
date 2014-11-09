using System;

namespace Fcaico.Controls.Calendar
{
	public static class DateTimeExtensions
	{
		public static bool IsSameDate(this DateTime date, DateTime comparedTo)
		{
			return date.Year == comparedTo.Year && date.Month == comparedTo.Month && date.Day == comparedTo.Day;
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeParser
{
    public class DateTime
    {
        public enum Months
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        public Dictionary<Months, int> MonthDays { get; } = new()
        {
            { Months.January, 31 },
            { Months.February, 28},
            { Months.March, 31 },
            { Months.April, 30 },
            { Months.May, 31 },
            { Months.June, 30 },
            { Months.July, 31 },
            { Months.August, 31 },
            { Months.September, 30 },
            { Months.October, 31 },
            { Months.November, 30 },
            { Months.December, 31 }
        };

        const int monthsInYear = 12;
        const int hoursInDay = 24;
        const int minutesInHour = 60;
        const int secondsInMinute = 60;

        public int DaysInYear
        {
            get
            {
                if (IsLeapYear)
                {
                    return 366;
                }
                else
                {
                    return 365;
                }
            }
        }
        public int DaysInMonth
        {
            get
            {
                int daysInMonth = MonthDays[(Months)month];
                if (month == (int)Months.February && IsLeapYear)
                {
                    ++daysInMonth;
                }
                return daysInMonth;
            }
        }

        public bool IsLeapYear
        {
            get
            {
                return (year % 400) == 0 || ((year % 4) == 0 && (year % 100) != 0);
            }
        }



        private int year;
        private int month;
        private int day;
        private int hour;
        private int minute;
        private int second;

        public int Year
        {
            get => year;
            set
            {
                year = 0;
                AddYears(value);
            }
        }
        public int Month
        {
            get => month;
            set
            {
                month = 0;
                AddMonths(value);
            }
        }
        public int Day
        {
            get => day;
            set
            {
                day = 0;
                AddDays(value);
            }
        }
        public int Hour
        {
            get => hour;
            set
            {
                hour = 0;
                AddDays(value);
            }
        }
        public int Minute
        {
            get => minute;
            set
            {
                minute = 0;
                AddMinutes(value);
            }
        }
        public int Second
        {
            get => second;
            set
            {
                second = 0;
                AddSeconds(value);
            }
        }

        /// <summary>
        /// Add years
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public int AddYears(int amount)
        {
            year += amount;
            AccountForLeapYearChange();
            return year;
        }

        /// <summary>
        /// Add months. If amount pushes months beyond number of months in a year, the correct number of years will be added as well.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public int AddMonths(int amount)
        {
            int tempMonth = month + amount;
            if (tempMonth > monthsInYear)
            {
                // Default int behaviour of truncating decimals down.
                int numOfYears = tempMonth / monthsInYear;
                AddYears(numOfYears);

                tempMonth -= numOfYears * monthsInYear;
            }
            month = tempMonth;
            AccountForLeapYearChange();
            return month;
        }

        /// <summary>
        /// Add days. If amount pushes days beyond days in month, the correct number of months will be added as well.
        /// </summary>
        /// <param name="amount"></param>
        public void AddDays(int amount)
        {
            int tempDay = day + amount;
            // Because days in the month changes for every month the month correction must be handled individually.
            while (tempDay > DaysInMonth)
            {
                tempDay -= DaysInMonth;
                AddMonths(1);
            }
            day = tempDay;
        }

        /// <summary>
        /// Add hours. If amount pushes days beyond hours in day, the correct number of days will be added as well.
        /// </summary>
        /// <param name="amount"></param>
        public void AddHours(int amount)
        {
            int tempHour = hour + amount;
            if (tempHour >= hoursInDay)
            {
                int numOfDays = tempHour / hoursInDay;
                AddDays(numOfDays);

                tempHour -= numOfDays * hoursInDay;
            }
            hour = tempHour;
        }

        /// <summary>
        /// Add minutes. If amount pushes minutes beyond minutes in hour, the correct number of hours will be added as well.
        /// </summary>
        /// <param name="amount"></param>
        public void AddMinutes(int amount)
        {
            int tempMinute = minute + amount;
            if (tempMinute >= minutesInHour)
            {
                int numOfHours = tempMinute / minutesInHour;
                AddHours(numOfHours);

                tempMinute -= numOfHours * minutesInHour;
            }
            minute = tempMinute;
        }

        /// <summary>
        /// Add seconds. If amount pushes seconds beyond seconds in minute, the correct number of minutes will be added as well.
        /// </summary>
        /// <param name="amount"></param>
        public void AddSeconds(int amount)
        {
            int tempSecond = second + amount;
            if (tempSecond >= secondsInMinute)
            {
                int numOfMinutes = tempSecond / secondsInMinute;
                AddMinutes(numOfMinutes);

                tempSecond -= numOfMinutes * secondsInMinute;
            }
            second = tempSecond;
        }

        /// <summary>
        /// Forces 29/2 from a leap year to 1/3 when switching to not a leap year.
        /// </summary>
        private void AccountForLeapYearChange()
        {
            if (!IsLeapYear && month == (int)Months.February && day == 29)
            {
                day = 1;
                month = (int)Months.March;
            }
        }
    }
}

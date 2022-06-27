using TimeParser;

namespace TimeParser.Test
{
    [TestClass]
    public class TimeParserTests
    {
        TimeParser timeParser = new();
        DateTime dateTime = new();

        [TestMethod]
        public void TestDateTimeAdd()
        {
            dateTime.Year = 2022;
            dateTime.Month = 6;
            dateTime.Day = 1;

            Assert.IsTrue(dateTime.Year == 2022);
            Assert.IsTrue(dateTime.Month == 6);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.AddYears(2);

            Assert.IsTrue(dateTime.Year == 2024);
            Assert.IsTrue(dateTime.Month == 6);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.AddMonths(14);

            Assert.IsTrue(dateTime.Year == 2025);
            Assert.IsTrue(dateTime.Month == 8);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.AddMonths(6);
            dateTime.AddDays(27);

            Assert.IsTrue(dateTime.Year == 2026);
            Assert.IsTrue(dateTime.Month == 2);
            Assert.IsTrue(dateTime.Day == 28);

            dateTime.AddDays(1);

            Assert.IsFalse(dateTime.IsLeapYear);
            Assert.IsTrue(dateTime.Year == 2026);
            Assert.IsTrue(dateTime.Month == 3);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.Month = 2;
            dateTime.AddDays(27);
            dateTime.AddYears(2);

            Assert.IsTrue(dateTime.IsLeapYear);
            Assert.IsTrue(dateTime.Year == 2028);
            Assert.IsTrue(dateTime.Month == 2);
            Assert.IsTrue(dateTime.Day == 28);

            dateTime.AddDays(1);

            Assert.IsTrue(dateTime.IsLeapYear);
            Assert.IsTrue(dateTime.Year == 2028);
            Assert.IsTrue(dateTime.Month == 2);
            Assert.IsTrue(dateTime.Day == 29);

            // Test leap year switch
            dateTime.AddYears(72);

            Assert.IsFalse(dateTime.IsLeapYear);
            Assert.IsTrue(dateTime.Year == 2100);
            Assert.IsTrue(dateTime.Month == 3);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.AddHours(6);

            Assert.IsTrue(dateTime.Year == 2100);
            Assert.IsTrue(dateTime.Month == 3);
            Assert.IsTrue(dateTime.Day == 1);
            Assert.IsTrue(dateTime.Hour == 6);

            dateTime.AddHours(24);

            Assert.IsTrue(dateTime.Year == 2100);
            Assert.IsTrue(dateTime.Month == 3);
            Assert.IsTrue(dateTime.Day == 2);
            Assert.IsTrue(dateTime.Hour == 6);

            dateTime.AddHours(50);

            Assert.IsTrue(dateTime.Year == 2100);
            Assert.IsTrue(dateTime.Month == 3);
            Assert.IsTrue(dateTime.Day == 4);
            Assert.IsTrue(dateTime.Hour == 8);

            dateTime.AddMinutes(50);

            Assert.IsTrue(dateTime.Year == 2100);
            Assert.IsTrue(dateTime.Month == 3);
            Assert.IsTrue(dateTime.Day == 4);
            Assert.IsTrue(dateTime.Hour == 8);
            Assert.IsTrue(dateTime.Minute == 50);

            dateTime.AddMinutes(11);

            Assert.IsTrue(dateTime.Year == 2100);
            Assert.IsTrue(dateTime.Month == 3);
            Assert.IsTrue(dateTime.Day == 4);
            Assert.IsTrue(dateTime.Hour == 9);
            Assert.IsTrue(dateTime.Minute == 1);

            dateTime.AddMinutes(24 * 61);

            Assert.IsTrue(dateTime.Year == 2100);
            Assert.IsTrue(dateTime.Month == 3);
            Assert.IsTrue(dateTime.Day == 5);
            Assert.IsTrue(dateTime.Hour == 9);
            Assert.IsTrue(dateTime.Minute == 25);
        }

        [TestMethod]
        public void TestDateTimeSubtract()
        {
            dateTime.Year = 2022;
            dateTime.Month = 6;
            dateTime.Day = 1;

            Assert.IsTrue(dateTime.Year == 2022);
            Assert.IsTrue(dateTime.Month == 6);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.AddYears(-2);

            Assert.IsTrue(dateTime.Year == 2020);
            Assert.IsTrue(dateTime.Month == 6);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.AddMonths(-14);

            Assert.IsTrue(dateTime.Year == 2019);
            Assert.IsTrue(dateTime.Month == 4);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.AddMonths(-4);

            Assert.IsTrue(dateTime.Year == 2018);
            Assert.IsTrue(dateTime.Month == 12);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.AddMonths(-38);

            Assert.IsTrue(dateTime.Year == 2015);
            Assert.IsTrue(dateTime.Month == 10);
            Assert.IsTrue(dateTime.Day == 1);

            dateTime.AddDays(-1);

            Assert.IsTrue(dateTime.Year == 2015);
            Assert.IsTrue(dateTime.Month == 9);
            Assert.IsTrue(dateTime.Day == 30);

            dateTime.AddHours(-1);

            Assert.IsTrue(dateTime.Year == 2015);
            Assert.IsTrue(dateTime.Month == 9);
            Assert.IsTrue(dateTime.Day == 29);
            Assert.IsTrue(dateTime.Hour == 23);

            dateTime.AddHours(-50);

            Assert.IsTrue(dateTime.Year == 2015);
            Assert.IsTrue(dateTime.Month == 9);
            Assert.IsTrue(dateTime.Day == 27);
            Assert.IsTrue(dateTime.Hour == 21);

            dateTime.AddMinutes(-50);

            Assert.IsTrue(dateTime.Year == 2015);
            Assert.IsTrue(dateTime.Month == 9);
            Assert.IsTrue(dateTime.Day == 27);
            Assert.IsTrue(dateTime.Hour == 20);
            Assert.IsTrue(dateTime.Minute == 10);

            dateTime.AddMinutes(-24 * 60);

            Assert.IsTrue(dateTime.Year == 2015);
            Assert.IsTrue(dateTime.Month == 9);
            Assert.IsTrue(dateTime.Day == 26);
            Assert.IsTrue(dateTime.Hour == 20);
            Assert.IsTrue(dateTime.Minute == 10);

            dateTime.AddMinutes(-50 * 60);

            Assert.IsTrue(dateTime.Year == 2015);
            Assert.IsTrue(dateTime.Month == 9);
            Assert.IsTrue(dateTime.Day == 24);
            Assert.IsTrue(dateTime.Hour == 18);
            Assert.IsTrue(dateTime.Minute == 10);

            dateTime.AddSeconds(-30);

            Assert.IsTrue(dateTime.Hour == 18);
            Assert.IsTrue(dateTime.Minute == 9);
            Assert.IsTrue(dateTime.Second == 30);

            dateTime.AddSeconds(-86400);

            Assert.IsTrue(dateTime.Month == 9);
            Assert.IsTrue(dateTime.Day == 23);
            Assert.IsTrue(dateTime.Hour == 18);
            Assert.IsTrue(dateTime.Minute == 9);
            Assert.IsTrue(dateTime.Second == 30);

            dateTime.Year = 2000;

            Assert.IsTrue(dateTime.IsLeapYear);

            dateTime.AddYears(-100);

            Assert.IsFalse(dateTime.IsLeapYear);

            dateTime.AddMinutes(4 * 365 * 24 * 60);

            Assert.IsTrue(dateTime.IsLeapYear);
        }
    }
}
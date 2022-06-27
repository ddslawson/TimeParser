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
        }
    }
}
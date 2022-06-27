using TimeParser;

namespace TimeParser.Test
{
    [TestClass]
    public class TimeParserTests
    {
        TimeParser timeParser = new();

        [TestMethod]
        public void TestMethod1()
        {
            string input = "";
            timeParser.Parse(input);
        }
    }
}
using TimeParser;

// Only works with one string command line arg. Ignore all others
if (args.Length > 0)
{
    string input = args[0];

    TimeParser.DateTime? output = TimeParser.TimeParser.Parse(input);
    string dateString = TimeParser.TimeParser.DateTimeToString(output);

    Console.WriteLine(dateString);
}
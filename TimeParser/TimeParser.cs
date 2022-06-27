namespace TimeParser
{
    public static class TimeParser
    {
        public enum UnitType
        {
            Year,
            Month,
            Day,
            Hour,
            Minute,
            Second
        }

        public enum OperatorType
        {
            Add,
            Subtract,
            Snap
        }

        static Dictionary<string, UnitType> unitMapping = new()
        {
            { "y", UnitType.Year },
            { "mon", UnitType.Month },
            { "d", UnitType.Day },
            { "h", UnitType.Hour },
            { "m", UnitType.Minute },
            { "s", UnitType.Second }
        };

        static Dictionary<char, OperatorType> operatorMapping = new()
        {
            { '+', OperatorType.Add },
            { '-', OperatorType.Subtract },
            { '@', OperatorType.Snap }
        };

        const string timeTag = "T";
        const string utcTimezone = "Z";
        const string nowFunctionTag = "now()";

        /// <summary>
        /// Parse string for time commands and modifiers.
        /// </summary>
        /// <param name="input">string to be parsed e.g. "now()-1d@d"</param>
        /// <returns>UTC</returns>
        public static DateTime? Parse(string input)
        {
            if (input.StartsWith(nowFunctionTag))
            {
                // Use System.DateTime to get system time in UTC
                var utcNow = System.DateTime.UtcNow;

                // Init custom datetime class
                DateTime dateTime = new()
                {
                    Year = utcNow.Year,
                    Month = utcNow.Month,
                    Day = utcNow.Day,
                    Hour = utcNow.Hour,
                    Minute = utcNow.Minute,
                    Second = utcNow.Second
                };

                bool parse = true;
                int? substringInclusiveStart = null;
                OperatorType? operatorType = null;
                UnitType? unitType = null;
                int? operationValue = null;

                for (int i = nowFunctionTag.Length; (i < input.Length) && parse; ++i)
                {
                    if (operatorType == null && operatorMapping.ContainsKey(input[i]))
                    {
                        operatorType = operatorMapping[input[i]];
                        substringInclusiveStart = i + 1;
                    }
                    else if (operatorType != null && substringInclusiveStart != null)
                    {
                        // If add/subtract and value is not yet found
                        if ((operatorType == OperatorType.Add || operatorType == OperatorType.Subtract) && operationValue == null)
                        {
                            string valueSubstring = string.Empty;
                            if (i + 1 >= input.Length)
                            {
                                // Invalid format. Still need unit.
                                parse = false;
                                break;
                            }
                            else if (!char.IsDigit(input[i+1]))
                            {
                                valueSubstring = input[substringInclusiveStart.Value..(i+1)];
                            }
                            

                            if (!string.IsNullOrEmpty(valueSubstring))
                            {
                                int value = 0;
                                if (int.TryParse(valueSubstring, out value))
                                {
                                    operationValue = value;
                                    substringInclusiveStart = i + 1;
                                }
                                else
                                {
                                    // Invalid format
                                    parse = false;
                                    break;
                                }
                            }
                        }
                        // If snap or if add/subtract and value has been found
                        else if (operatorType == OperatorType.Snap ||
                            (operatorType == OperatorType.Add || operatorType == OperatorType.Subtract) && operationValue != null)
                        {
                            string unitSubstring = string.Empty;
                            if (i + 1 >= input.Length)
                            {
                                unitSubstring = input[substringInclusiveStart.Value..];
                            }
                            else if (!char.IsLetter(input[i+1]))
                            {
                                unitSubstring = input[substringInclusiveStart.Value..(i+1)];
                            }

                            if (!string.IsNullOrEmpty(unitSubstring))
                            {
                                if (unitMapping.ContainsKey(unitSubstring))
                                {
                                    unitType = unitMapping[unitSubstring];
                                    substringInclusiveStart = i + 1;
                                }
                                else
                                {
                                    // Invalid format
                                    parse = false;
                                    break;
                                }
                            }
                        }
                    }

                    // If operation and unit has been found.
                    if (operatorType != null && unitType != null)
                    {
                        if (operatorType == OperatorType.Snap)
                        {

                            switch (unitType)
                            {
                                case UnitType.Year:
                                    dateTime = new()
                                    {
                                        Year = dateTime.Year
                                    };
                                    break;
                                case UnitType.Month:
                                    dateTime = new()
                                    {
                                        Year = dateTime.Year,
                                        Month = dateTime.Month
                                    };
                                    break;
                                case UnitType.Day:
                                    dateTime = new()
                                    {
                                        Year = dateTime.Year,
                                        Month = dateTime.Month,
                                        Day = dateTime.Day
                                    };
                                    break;
                                case UnitType.Hour:
                                    dateTime = new()
                                    {
                                        Year = dateTime.Year,
                                        Month = dateTime.Month,
                                        Day = dateTime.Day,
                                        Hour = dateTime.Hour
                                    };
                                    break;
                                case UnitType.Minute:
                                    dateTime = new()
                                    {
                                        Year = dateTime.Year,
                                        Month = dateTime.Month,
                                        Day = dateTime.Day,
                                        Hour = dateTime.Hour,
                                        Minute = dateTime.Minute
                                    };
                                    break;
                                case UnitType.Second:
                                    // Smallest unit can't snap.
                                    break;
                                default:
                                    // Should be impossible to reach here.
                                    parse = false;
                                    break;
                            }
                        }
                        else if ((operatorType == OperatorType.Add || operatorType == OperatorType.Subtract) && operationValue != null)
                        {
                            int factor = operatorType == OperatorType.Add ? 1 : -1;
                            
                            switch (unitType)
                            {
                                case UnitType.Year:
                                    dateTime.AddYears(operationValue.Value * factor);
                                    break;
                                case UnitType.Month:
                                    dateTime.AddMonths(operationValue.Value * factor);
                                    break;
                                case UnitType.Day:
                                    dateTime.AddDays(operationValue.Value * factor);
                                    break;
                                case UnitType.Hour:
                                    dateTime.AddHours(operationValue.Value * factor);
                                    break;
                                case UnitType.Minute:
                                    dateTime.AddMinutes(operationValue.Value * factor);
                                    break;
                                case UnitType.Second:
                                    dateTime.AddSeconds(operationValue.Value * factor);
                                    break;
                                default:
                                    // Should be impossible to reach here.
                                    parse = false;
                                    break;
                            }
                        }
                        else
                        {
                            // Should be impossible to reach here.
                            parse = false;
                            break;
                        }

                        operatorType = null;
                        operationValue = null;
                        unitType = null;
                    }
                }
                return dateTime;
            }
            return null;
        }

        public static string DateTimeToString(DateTime dateTime)
        {
            if (dateTime != null)
            {
                return $"{dateTime.Year:D4}-{dateTime.Month:D2}-{dateTime.Day:D2}{timeTag}{dateTime.Hour:D2}:{dateTime.Minute:D2}:{dateTime.Second:D2}{utcTimezone}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
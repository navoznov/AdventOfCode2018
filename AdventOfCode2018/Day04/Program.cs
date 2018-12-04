using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            program.Solve1();
            program.Solve2();
        }

        private void Solve1()
        {
            var lines = File.ReadAllLines("input1.txt");

            var guardSchedules = GetGuardSchedules(lines);

            var globalMaxMinutes = 0;
            var maxAsleepGuardId = 0;
            var globalMaxMinuteIndex = 0;

            foreach (var guard in guardSchedules)
            {
                var hour = guard.Value;
                var minutesSum = hour.Sum();
                if (minutesSum <= globalMaxMinutes)
                {
                    continue;
                }

                globalMaxMinutes = minutesSum;
                maxAsleepGuardId = guard.Key;

                var maxMinute = 0;
                for (var i = 0; i < hour.Length; i++)
                {
                    var minute = hour[i];
                    if (minute > maxMinute)
                    {
                        maxMinute = minute;
                        globalMaxMinuteIndex = i;
                    }
                }
            }

            Console.WriteLine(globalMaxMinuteIndex * maxAsleepGuardId);
        }

        private Dictionary<int, int[]> GetGuardSchedules(string[] lines)
        {
            lines = lines.OrderBy(x => x).ToArray();

            var guardSchedules = new Dictionary<int, int[]>();
            var linesCounter = 0;
            var currentGuardId = -1;
            while (linesCounter < lines.Length)
            {
                var line = lines[linesCounter];

                if (TryParseGuardLine(line, out var _, out var parsedGuardId))
                {
                    currentGuardId = parsedGuardId;
                    if (!guardSchedules.ContainsKey(currentGuardId))
                    {
                        guardSchedules[currentGuardId] = new int[60];
                    }
                }
                else if (TryParseActionLine(line, out var fallAsleepDateTime))
                {
                    linesCounter++;
                    if (TryParseActionLine(lines[linesCounter], out var awakeDateTime))
                    {
                        var fallAsleepMinutes = CorrectDateTime(fallAsleepDateTime).Minute;
                        var awakeMinutes = CorrectDateTime(awakeDateTime).Minute;
                        var guardSchedule = guardSchedules[currentGuardId];
                        for (var i = fallAsleepMinutes; i < awakeMinutes; i++)
                        {
                            guardSchedule[i] = guardSchedule[i] + 1;
                        }
                    }
                }

                linesCounter++;
            }

            return guardSchedules;
        }

        private static bool TryParseGuardLine(string line, out DateTime dateTime, out int guardId)
        {
            const string pattern = @"\[(?<actionDateTime>\d+-\d+-\d+ \d+:\d+)\] Guard #(?<guardId>\d+) begins shift";
            var match = Regex.Match(line, pattern);
            if (!match.Success)
            {
                guardId = -1;
                dateTime = DateTime.MinValue;
                return false;
            }
            var dateTimeStr = match.Groups["actionDateTime"].Value;
            guardId = int.Parse(match.Groups["guardId"].Value);
            dateTime = DateTime.ParseExact(dateTimeStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            return true;
        }

        private static bool TryParseActionLine(string line, out DateTime dateTime)
        {
            var pattern = @"\[(?<actionDateTime>\d+-\d+-\d+ \d+:\d+)\] (?<actionText>[a-z]+)";
            var match = Regex.Match(line, pattern);
            if (!match.Success)
            {
                dateTime = DateTime.MinValue;
                return false;
            }

            var dateTimeStr = match.Groups["actionDateTime"].Value;
            dateTime = DateTime.ParseExact(dateTimeStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            return true;
        }

        DateTime CorrectDateTime(DateTime dateTime)
        {
            return dateTime.Hour > 0 ? new DateTime(dateTime.Year, dateTime.Month, dateTime.Day+1, 0, 0, 0) : dateTime;
        }

        private void Solve2()
        {
            var lines = File.ReadAllLines("input1.txt");
            var guardSchedules = GetGuardSchedules(lines);

            var maxMinuteGuardId = -1;
            var maxMinuteIndex = -1;
            var maxMinute = 0;
            foreach (var guardSchedule in guardSchedules)
            {
                var guardId = guardSchedule.Key;
                var hour = guardSchedule.Value;
                for (int i = 0; i < hour.Length; i++)
                {
                    var minute = hour[i];
                    if (minute > maxMinute)
                    {
                        maxMinute = minute;
                        maxMinuteGuardId = guardId;
                        maxMinuteIndex = i;
                    }
                }
            }

            Console.WriteLine(maxMinuteGuardId * maxMinuteIndex);
        }

    }
}

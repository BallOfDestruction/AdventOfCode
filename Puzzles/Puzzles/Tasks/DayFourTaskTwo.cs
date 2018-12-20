using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/4
    /// </summary>
    public class DayFourTaskTwo : ITask
    {
        public string Solve(string input)
        {
            var data = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(w => new Duty(w))
                            .OrderBy(w => w.DateTime)
                            .ToList();

            var allDates = data.Select(w => w.DateTime).ToList();
            var minDate = allDates.Min();
            var maxDate = allDates.Max();

            // Not sure about count days, but can take more
            var countDays = (int)(maxDate.Date - minDate.Date).TotalDays + 2;

            var sleepGuardInfo = new int[countDays, 60];

            var guardId = -1;
            Duty lastSleepDuty = null;

            // FILL THE DATA
            // Исходя из данных, нет таких случаев, когда стражник проспал сутки,
            // т.е. начал спать в один день, а проснулся в другой
            foreach (var duty in data)
            {
                switch (duty.Operation)
                {
                    case Operation.BeginShift:
                        guardId = duty.GuardId;
                        break;
                    case Operation.FallsAsleep:
                        lastSleepDuty = duty;
                        break;
                    case Operation.WakesUp:
                        if (lastSleepDuty != null)
                        {
                            var startDate = lastSleepDuty.DateTime;
                            var endDate = duty.DateTime;
                            var dayIndex = (startDate.Date - minDate.Date).Days;

                            for (var i = startDate.Minute; i < endDate.Minute; i++)
                            {
                                sleepGuardInfo[dayIndex, i] = guardId;
                            }
                        }
                        break;
                }
            }

            var sleepMinutesByGuard = new CountSleepMinutes[60];
            for (var minute = 0; minute < 60; minute++)
            {
                var sleepingGuardInThisMinute = new List<int>();
                for (var day = 0; day < countDays; day++)
                {
                    var sleepGuardId = sleepGuardInfo[day, minute];
                    if(sleepGuardId != 0)
                        sleepingGuardInThisMinute.Add(sleepGuardId);
                }

                if (sleepingGuardInThisMinute.Any())
                {

                    sleepMinutesByGuard[minute] = sleepingGuardInThisMinute.GroupBy(w => w)
                        .Select(w => new CountSleepMinutes(w.Key, w.Count(), minute))
                        .OrderByDescending(w => w.Count)
                        .First();
                }
            }

            var mostlyCountItem = sleepMinutesByGuard.Where(w => w != null).OrderByDescending(w => w.Count).First();

            return (mostlyCountItem.Minute * mostlyCountItem.GuardId).ToString();
        }

        private class CountSleepMinutes
        {
            public int GuardId { get; }
            public int Count { get;  }
            public int Minute { get; }

            public CountSleepMinutes(int guardId, int count, int minute)
            {
                GuardId = guardId;
                Count = count;
                Minute = minute;
            }
        }

        private class Duty
        {
            public int Year { get; }
            public int Month { get; }
            public int Day { get; }

            public int Hours { get; }
            public int Minutes { get; }

            public string Info { get; }

            public DateTime DateTime { get; }

            public int GuardId { get; } = -1;

            public Operation Operation { get; }

            public Duty(string data)
            {
                // [1518-03-20 23:58] Guard #73 begins shift
                // [1518-11-01 00:05] falls asleep
                // 012345678901234567890123456789
                Year = int.Parse($"{data[1]}{data[2]}{data[3]}{data[4]}");
                Month = int.Parse($"{data[6]}{data[7]}");
                Day = int.Parse($"{data[9]}{data[10]}");

                Hours = int.Parse($"{data[12]}{data[13]}");
                Minutes = int.Parse($"{data[15]}{data[16]}");

                Info = data.Substring(19);

                DateTime = new DateTime(Year, Month, Day, Hours, Minutes, 0);

                var indexId = Info.IndexOf('#');
                if (indexId != -1)
                {
                    var substring = Info.Substring(indexId + 1);
                    var indexOfSpace = substring.IndexOf(' ');
                    var id = substring.Substring(0, indexOfSpace + 1);
                    GuardId = int.Parse(id);
                }

                if (data.Contains("begins shift"))
                {
                    Operation = Operation.BeginShift;
                }
                else if (data.Contains("falls asleep"))
                {
                    Operation = Operation.FallsAsleep;
                }
                else if (data.Contains("wakes up"))
                {
                    Operation = Operation.WakesUp;
                }
            }

            public override string ToString()
            {
                return $"[{Year:0000}-{Month:00}-{Day:00} {Hours:00}:{Minutes:00}] {Info}";
            }
        }

        private enum Operation
        {
            BeginShift,
            FallsAsleep,
            WakesUp
        }
    }
}

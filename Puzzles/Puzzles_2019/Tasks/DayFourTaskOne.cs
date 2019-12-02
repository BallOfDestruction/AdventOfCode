using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/4
    /// </summary>
    public class DayFourTaskOne : ITask
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

            var countSleepMinutes = new Dictionary<int, int>();
            for (var i = 0; i < countDays; i++)
            {
                for (var j = 0; j < 60; j++)
                {
                    var sleepingGuardId = sleepGuardInfo[i, j];
                    if (sleepingGuardId == 0 || sleepingGuardId == -1) continue;

                    if (countSleepMinutes.ContainsKey(sleepingGuardId))
                        countSleepMinutes[sleepingGuardId]++;
                    else
                        countSleepMinutes.Add(sleepingGuardId, 1);
                }
            }

            var countSleepDayPerMinute = new int[60];
            var theMostSleepGuardId = countSleepMinutes.OrderByDescending(w => w.Value).First().Key;
            for (var i = 0; i < 60; i++)
            {
                var countSleepDayByThisGuard = 0;

                for (var j = 0; j < countDays; j++)
                {
                    if (sleepGuardInfo[j, i] == theMostSleepGuardId)
                        countSleepDayByThisGuard++;
                }

                countSleepDayPerMinute[i] = countSleepDayByThisGuard;
            }

            var mostlySleepCount = countSleepDayPerMinute.Max();
            var mostlySleepMinute = countSleepDayPerMinute.ToList().IndexOf(mostlySleepCount);

            return (theMostSleepGuardId * mostlySleepMinute).ToString();
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

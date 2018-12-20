using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/4
    /// </summary>
    public class DayFourTaskOne : ITask
    {
        public string Solve(string input)
        {
            var data = input.Split(new [] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries)
                            .Select(w => new Duty(w))
                            .OrderBy(w => w.DateTime)
                            .ToList();

            var dates = data.Select(w => w.DateTime).ToList();
            var minDate = dates.Min();
            var maxDate = dates.Max();

            var offset = maxDate.Date - minDate.Date;
            // Not sure about count days, but can take more
            var days = (int)offset.TotalDays + 2;

            var arrays = new int[days, 60];

            var currentID = -1;
            Duty dutySleep = null;

            // FILL THE DATA
            // Исходя из данных, нет таких случаев, когда стражник проспал сутки,
            // т.е. начал спать в один день, а проснулся в другой
            foreach (var duty in data)
            {
                switch (duty.Operation)
                {
                    case Operation.BeginShift:
                        currentID = duty.GuardId;
                        break;
                    case Operation.FallsAsleep:
                        dutySleep = duty;
                        break;
                    case Operation.WakesUp:
                        if (dutySleep != null)
                        {
                            var startDate = dutySleep.DateTime;
                            var endDate = duty.DateTime;
                            var dayIndex = (startDate.Date - minDate.Date).Days;

                            for (var i = startDate.Minute; i < endDate.Minute; i++)
                            {
                                arrays[dayIndex, i] = currentID;
                            }
                        }
                        break;
                }
            }
            
            var dicDutyById = new Dictionary<int, int>();
            for (var i = 0; i < days; i++)
            {
                for (var j = 0; j < 60; j++)
                {
                    var item = arrays[i, j];
                    if (item == 0 || item == -1) continue;

                    if (dicDutyById.ContainsKey(item))
                        dicDutyById[item]++;
                    else
                        dicDutyById.Add(item, 1);
                }
            }

            var array = new int[60];
            var mostlyItem = dicDutyById.OrderByDescending(w => w.Value).First().Key;
            for (var i = 0; i < 60; i++)
            {
                var count = 0;

                for (var j = 0; j < days; j++)
                {
                    if (arrays[j, i] == mostlyItem)
                        count++;
                }

                array[i] = count;
            }

            var max = array.Max();
            var minute = array.ToList().IndexOf(max);

            return (mostlyItem * minute).ToString();
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

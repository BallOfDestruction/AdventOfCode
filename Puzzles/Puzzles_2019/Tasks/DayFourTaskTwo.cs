using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/4
    /// </summary>
    public class DayFourTaskTwo : ITask
    {
        public string Solve(string input)
        {
            var intes = input.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var start = intes[0];
            var end = intes[1];

            var count = 0;
            
            for (var i = start; i < end; i++)
            {
                var digits = new List<int>();

                var localNumber = i;
                
                while (localNumber > 0) {
                    digits.Insert(0, localNumber % 10);
                    localNumber /= 10;
                }

                var isGood = true;
                
                for (var j = 0; j < digits.Count - 1; j++)
                {
                    if (digits[j] > digits[j + 1])
                    {
                        isGood = false;
                        break;
                    }
                }

                if (isGood)
                {
                    isGood = digits.GroupBy(w => w).Any(w => w.Count() == 2);
                }

                if (isGood)
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}

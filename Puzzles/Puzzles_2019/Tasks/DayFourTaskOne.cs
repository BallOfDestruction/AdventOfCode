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
            var intes = input.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var start = intes[0];
            var end = input[1];

            var count = 0;
            
            for (var i = start; i <= end; i++)
            {
                var digits = new List<int>();

                var localNumber = i;
                
                while (localNumber > 0) {
                    digits.Add(localNumber % 10);
                    localNumber /= 10;
                }
            }

            return count.ToString();
        }
    }
}

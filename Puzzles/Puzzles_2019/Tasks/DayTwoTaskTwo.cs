using System;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/2
    /// </summary>
    public class DayTwoTaskTwo : ITask
    {
        public string Solve(string input)
        {
            var allStrings = input.Split(new[] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);

            var answerList = (from item1 in allStrings.AsEnumerable()
                    from item2 in allStrings.AsEnumerable()
                    where item1 != item2
                    select new string(item1.Where((t, i) => t == item2[i]).ToArray()))
                .OrderByDescending(w => w.Length);

            return answerList.FirstOrDefault();
        }
    }
}

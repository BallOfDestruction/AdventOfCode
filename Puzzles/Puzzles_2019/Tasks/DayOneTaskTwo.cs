using System;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
	/// <summary>
	/// https://adventofcode.com/2019/day/1
	/// </summary>
	public class DayOneTaskTwo : ITask
	{
		public string Solve(string input)
		{
            var allSequence = input.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var sum = allSequence.Select(FindFuels).Sum();
            return sum.ToString();
        }

        private int FindFuels(int fuel)
        {
            var needFuel = (fuel / 3) - 2;

            return needFuel <= 0 ? 0 : needFuel + FindFuels(needFuel);
        }
	}
}

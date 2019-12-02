using System;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
	/// <summary>
	/// https://adventofcode.com/2019/day/1
	/// </summary>
	public class DayOneTaskOne : ITask
	{
		public string Solve(string input)
		{
			var allSequence = input.Split(new[] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var sum = allSequence.Select(w => w / 3 - 2).Sum();
			return sum.ToString();
		}
	}
}

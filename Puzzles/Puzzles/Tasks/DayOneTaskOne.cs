using System;
using System.Linq;

namespace Puzzles.Tasks
{
	/// <summary>
	/// https://adventofcode.com/2018/day/1
	/// </summary>
	public class DayOneTaskOne : ITask
	{
		public string Solve(string input)
		{
			var allSequence = input.Split(new[] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
			var sum = allSequence.Sum();
			return sum.ToString();
		}
	}
}

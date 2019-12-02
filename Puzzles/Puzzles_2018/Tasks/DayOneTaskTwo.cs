using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2018.Tasks
{
	/// <summary>
	/// https://adventofcode.com/2018/day/1
	/// </summary>
	public class DayOneTaskTwo : ITask
	{
		public string Solve(string input)
		{
			var allInt = input.Split(new[] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries)
							  .Select(int.Parse)
							  .ToList();

			var lastAnswer = 0;
			var frequency = new HashSet<int>() {lastAnswer};

			while (true)
			{
				foreach (var i in allInt)
				{
					lastAnswer += i;

					if (frequency.Contains(lastAnswer))
						return lastAnswer.ToString();

					frequency.Add(lastAnswer);
				}
			}
		}
	}
}

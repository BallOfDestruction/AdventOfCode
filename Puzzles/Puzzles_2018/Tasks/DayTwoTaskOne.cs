using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2018.Tasks
{
	/// <summary>
	/// https://adventofcode.com/2018/day/2
	/// </summary>
	public class DayTwoTaskOne : ITask
	{
		public string Solve(string input)
		{
			var allIds = input.Split(new[] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);

			var items = new List<int>();
			foreach (var allString in allIds)
			{
				var counts = allString.Distinct().Select(item => allString.Count(w => w == item)).Distinct();
				items.AddRange(counts);
			}

			var answer = (from item in items.Distinct() where item != 1 select items.Count(w => w == item)).ToList();

			var answerInt = answer.Aggregate(1, (i, i1) => i * i1);

			return answerInt.ToString();
		}
	}
}

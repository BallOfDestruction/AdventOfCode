using System;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
	/// <summary>
	/// https://adventofcode.com/2019/day/2
	/// </summary>
	public class DayTwoTaskOne : ITask
	{
		public string Solve(string input)
		{
			var array = input.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            array[1] = 12;
            array[2] = 2;

            for (var index = 0; index < array.Length; )
            {
                var item = array[index];

                if (item == 1 || item == 2)
                {
                    var firstNumber = array[array[index + 1]];
                    var secondNumner = array[array[index + 2]];
                    var result = item == 1 ? firstNumber + secondNumner : firstNumber * secondNumner;
                    array[array[index + 3]] = result;
                    index += 4;
                }
                else
                {
                    break;
                }
            }

            return array[0].ToString();
        }
	}
}

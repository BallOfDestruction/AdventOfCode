using System;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2019day/2
    /// </summary>
    public class DayTwoTaskTwo : ITask
    {
        private const int NEEDED_NUMBER = 19690720;

        public string Solve(string input)
        {
            var array = input.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var localArray = array.ToArray();

                    localArray[1] = i;
                    localArray[2] = j;

                    for (var index = 0; index < localArray.Length;)
                    {
                        var item = localArray[index];

                        if (item == 1 || item == 2)
                        {
                            var firstNumber = localArray[localArray[index + 1]];
                            var secondNumner = localArray[localArray[index + 2]];
                            var result = item == 1 ? firstNumber + secondNumner : firstNumber * secondNumner;
                            localArray[localArray[index + 3]] = result;
                            index += 4;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (localArray[0] == NEEDED_NUMBER)
                    {
                        return (i * 100 + j).ToString();
                    }
                }
            }

            throw new Exception("NoAnswer");
        }
    }
}
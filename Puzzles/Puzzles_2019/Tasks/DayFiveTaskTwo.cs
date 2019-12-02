using System;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/5
    /// </summary>
    public class DayFiveTaskTwo : ITask
    {
        public string Solve(string input)
        {
            var distinctUnits = input.Distinct();

            var min = distinctUnits.Select(w => React(RemoveUnit(input, w))).Select(w => w.Length).Min();

            return min.ToString();
        }

        private string RemoveUnit(string input, char unit)
        {
            var lowerUnit = char.ToLower(unit);
            var inputList = input.ToList();
            inputList.RemoveAll((c => char.ToLower(c) == lowerUnit));
            return new string(inputList.ToArray());
        }

        private string React(string input)
        {
            var removeReactPolymers = RemoveReactPolymers(input);

            return removeReactPolymers;
        }

        private string RemoveReactPolymers(string input)
        {
            for (var index = 0; index < input.Length - 1; index++)
            {
                var firstChar = input[index];
                var secondChar = input[index + 1];

                var isEquals = firstChar.ToString().Equals(secondChar.ToString(), StringComparison.InvariantCultureIgnoreCase);
                if (!isEquals)
                    continue;

                var firstRegister = char.IsLower(firstChar);
                var secondRegister = char.IsLower(secondChar);

                var isOpposite = firstRegister && !secondRegister || !firstRegister && secondRegister;

                if (isOpposite)
                {
                    input = input.Remove(index, 2);
                    index -= 2;
                    if (index < 0)
                        index = 0;
                }
            }

            return input;
        }
    }
}

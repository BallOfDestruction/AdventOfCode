using System;
using System.Linq;

namespace Puzzles.Tasks
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
            var longString = input;

            while (true)
            {
                var removeReactPolymers = RemoveReactPolymers(longString);

                if (removeReactPolymers == longString)
                    break;

                longString = removeReactPolymers;
            }

            return longString;
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
                    return input.Remove(index, 2);
                }
            }

            return input;
        }
    }
}

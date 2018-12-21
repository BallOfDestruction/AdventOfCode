using System;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/5
    /// </summary>
    public class DayFiveTaskOne : ITask
    {
        public string Solve(string input)
        {
            var removeReactPolymers = RemoveReactPolymers(input);

            return removeReactPolymers.Length.ToString();
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

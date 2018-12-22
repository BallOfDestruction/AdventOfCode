using System;
using System.IO;

namespace Puzzles
{
    public class Solver
    {
        public string TrySolve(TaskData taskData)
        {
            var inputString = File.ReadAllText($"DataTest\\{taskData.Data}.txt");

            var solve = Solve(taskData, inputString);

            return solve;
        }

        protected virtual string Solve(TaskData taskData, string input)
        {
            return taskData.Task.Solve(input);
        }

        public bool CheckSolve(TaskData taskData)
        {
            var inputString = File.ReadAllText($"DataTest\\{taskData.Data}.txt");
            var answer = File.ReadAllText($"DataAnswer\\{taskData.Answer}.txt");

            var solve = Solve(taskData, inputString);

            return solve.Equals(answer, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

using System;
using System.IO;

namespace Shared.Solvers
{
    public class Solver
    {
        public string TrySolve(TaskData taskData)
        {
            var inputString = File.ReadAllText(GetInput(taskData));

            var solve = Solve(taskData, inputString);

            return solve;
        }

        protected virtual string Solve(TaskData taskData, string input)
        {
            return taskData.Task.Solve(input);
        }

        public bool CheckSolve(TaskData taskData)
        {
            var inputString = File.ReadAllText(GetInput(taskData));
            var answer = File.ReadAllText(GetOutput(taskData));

            var solve = Solve(taskData, inputString);

            return solve.Equals(answer, StringComparison.InvariantCultureIgnoreCase);
        }

        private string GetInput(TaskData taskData)
        {
            return Path.Combine("DataTest", $"{taskData.Data}.txt");
        }

        private string GetOutput(TaskData taskData)
        {
            return Path.Combine("DataAnswer", $"{taskData.Data}.txt");
        }
    }
}

using System;
using System.IO;

namespace Puzzles
{
    public class Solver
    {
        public string TrySolve(TaskData taskData)
        {
            var inputString = File.ReadAllText($"DataTest\\{taskData.Data}.txt");

            var solve = taskData.Task.Solve(inputString);

            return solve;
        }

        public bool CheckSolve(TaskData taskData)
        {
            var inputString = File.ReadAllText($"DataTest\\{taskData.Data}.txt");
            var answer = File.ReadAllText($"DataAnswer\\{taskData.Answer}.txt");

            var solve = taskData.Task.Solve(inputString);

            return solve.Equals(answer, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

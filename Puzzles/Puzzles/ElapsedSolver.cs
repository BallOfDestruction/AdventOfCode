using System;
using System.Diagnostics;

namespace Puzzles
{
    public class ElapsedSolver : Solver
    {
        protected override string Solve(TaskData taskData, string input)
        {
            var timer = new Stopwatch();
            timer.Start();

            var answer = base.Solve(taskData, input);

            timer.Stop();

            Console.WriteLine($"[{taskData.Task.GetType().Name}] Elapsed time: {timer.ElapsedMilliseconds} mls");

            return answer;
        }
    }
}

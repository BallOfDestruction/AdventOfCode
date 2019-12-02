using System;
using System.Collections.Generic;
using Puzzles_2019.Tasks;
using Shared.Solvers;

namespace Puzzles_2019
{
	public static class Program
	{
        private static readonly List<TaskData> Tasks = new List<TaskData>()
        {
            new TaskData(new DayOneTaskOne(), "task11","task11"),
            new TaskData(new DayOneTaskTwo(), "task12","task12"),

            new TaskData(new DayTwoTaskOne(), "task21","task21"),
            new TaskData(new DayTwoTaskTwo(), "task22","task22"),

            new TaskData(new DayThreeTaskOne(), "task31","task31"),
            new TaskData(new DayThreeTaskTwo(), "task32","task32"),

            new TaskData(new DayFourTaskOne(), "task41","task41"),
            new TaskData(new DayFourTaskTwo(), "task42","task42"),

            new TaskData(new DayFiveTaskOne(), "task51","task51"),
            new TaskData(new DayFiveTaskTwo(), "task52","task52"),

            new TaskData(new DaySixTaskOne(), "task61","task61"),
            new TaskData(new DaySixTaskTwo(), "task62","task62"),

            new TaskData(new DaySevenTaskOne(), "task71","task71"),
            new TaskData(new DaySevenTaskTwo(), "task72","task72"),

            new TaskData(new DayEightTaskOne(), "task81","task81"),
            new TaskData(new DayEightTaskTwo(), "task82","task82"),

            new TaskData(new DayNineTaskOne(), "task91","task91"),
            new TaskData(new DayNineTaskTwo(), "task92","task92"),

            new TaskData(new DayTenTaskOne(), "task10_1","task10_1"),
            new TaskData(new DayTenTaskTwo(), "task10_2","task10_2"),

            new TaskData(new DayElevenTaskOne(), "task11_1","task11_1"),
            new TaskData(new DayElevenTaskTwo(), "task11_2","task11_2"),

            new TaskData(new DayTwelveTaskOne(), "task12_1","task12_1"),
            new TaskData(new DayTwelveTaskTwo(), "task12_2","task12_2"),
        };

        static void Main()
        {
            //CheckAll();

            var solver = new ElapsedSolver();

            var task = new DayTwoTaskOne();
            var taskData = "task21";

            var data = new TaskData(task, taskData, taskData);

            var answer = solver.TrySolve(data);
            Console.WriteLine(answer);
            Console.ReadLine();
        }

        // ReSharper disable once UnusedMember.Local
        private static void TrySolveAll()
        {
            var solver = new ElapsedSolver();

            Tasks.ForEach(w => Console.WriteLine(solver.TrySolve(w)));

            Console.ReadLine();
        }

        // ReSharper disable once UnusedMember.Local
        private static void CheckAll()
        {
            var solver = new ElapsedSolver();

            Tasks.ForEach(w => Console.WriteLine(solver.CheckSolve(w)));

            Console.ReadLine();
        }
    }
}

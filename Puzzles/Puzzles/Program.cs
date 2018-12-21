using System;
using System.Collections.Generic;
using Puzzles.Tasks;

namespace Puzzles
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
        };

        static void Main(string[] args)
        {
            //CheckAll();

            var solver = new Solver();

            var task = new DaySixTaskTwo();
            var taskData = "task62";


            var data = new TaskData(task, taskData);

            var answer = solver.TrySolve(data);
            Console.WriteLine(answer);
            Console.ReadLine();
        }

        private static void TrySolveAll()
        {
            var solver = new Solver();

            Tasks.ForEach(w => Console.WriteLine(solver.TrySolve(w)));

            Console.ReadLine();
        }

        private static void CheckAll()
        {
            var solver = new Solver();

            Tasks.ForEach(w => Console.WriteLine(solver.CheckSolve(w)));

            Console.ReadLine();
        }
    }
}

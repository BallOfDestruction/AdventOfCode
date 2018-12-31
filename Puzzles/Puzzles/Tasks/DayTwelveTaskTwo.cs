using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/12
    /// </summary>
    public class DayTwelveTaskTwo : ITask
    {
        private const int InputLength = 5;
        private const long CountGeneration = 50000000000;

        public string Solve(string input)
        {
            var splitData = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var initData = splitData[0].Replace("initial state: ", "");
            var rules = splitData.Skip(1).ToList();

            var container = new LineContainer(initData);
            var rulesList = rules.Select(w => new Rule(w)).ToList();

            for (var generation = 0L; generation < CountGeneration; generation++)
            {
                var nextGeneration = container.Position.Select(w => w.Copy()).ToList();

                if (generation % 100 == 0)
                {
                    Console.WriteLine(generation);
                    Console.WriteLine(container.ToAnswer());
                }

                //Console.WriteLine(container.Position.Aggregate("", (s, cell) => s + (cell.IsPlant ? "#" : ".")));

                for (var i = 2; i < container.Position.Count - 2; i++)
                {
                    var prePreCell = container.Position[i - 2];
                    var preCell = container.Position[i - 1];
                    var currentCell = container.Position[i];
                    var nextCell = container.Position[i + 1];
                    var nextNextCell = container.Position[i + 2];

                    var rightRules = rulesList.Where(w => w.IsRight(prePreCell.IsPlant, preCell.IsPlant,
                        currentCell.IsPlant, nextCell.IsPlant, nextNextCell.IsPlant)).ToList();

                    var currentRule = rightRules.FirstOrDefault();
                    if (currentRule != null)
                        nextGeneration[i].IsPlant = currentRule.Output;
                }

                var firstIsPlant = nextGeneration.Take(InputLength).TakeWhile(w => !w.IsPlant).ToList();
                if (firstIsPlant.Count < InputLength)
                {
                    var first = nextGeneration.First();
                    for (var i = 0; i < InputLength - firstIsPlant.Count; i++)
                    {
                        nextGeneration.Insert(0, new Cell(first.Position - i - 1, '.'));
                    }
                }

                var endIsPlant = nextGeneration.TakeLast(InputLength).ToList();
                endIsPlant.Reverse();
                endIsPlant = endIsPlant.TakeWhile(w => !w.IsPlant).ToList();
                if (endIsPlant.Count < InputLength)
                {
                    var last = nextGeneration.Last();
                    for (var i = 0; i < InputLength - endIsPlant.Count; i++)
                    {
                        nextGeneration.Add(new Cell(last.Position + i + 1, '.'));
                    }
                }

                container.Position = nextGeneration;
            }

            return container.ToAnswer().ToString();
        }

        private class LineContainer
        {
            public List<Cell> Position { get; set; } = new List<Cell>();

            public LineContainer(string initData)
            {
                Position.Add(new Cell(-5, '.'));
                Position.Add(new Cell(-4, '.'));
                Position.Add(new Cell(-3, '.'));
                Position.Add(new Cell(-2, '.'));
                Position.Add(new Cell(-1, '.'));

                for (var index = 0; index < initData.Length; index++)
                {
                    Position.Add(new Cell(index, initData[index]));
                }

                Position.Add(new Cell(initData.Length, '.'));
                Position.Add(new Cell(initData.Length + 1, '.'));
                Position.Add(new Cell(initData.Length + 2, '.'));
                Position.Add(new Cell(initData.Length + 3, '.'));
                Position.Add(new Cell(initData.Length + 4, '.'));
            }

            public long ToAnswer()
            {
                return Position.Where(w => w.IsPlant).Sum(w => w.Position);
            }
        }

        private class Rule
        {
            public bool[] Input { get; set; }
            public bool Output { get; set; }

            public Rule(string data)
            {
                var splitData = data.Split(new char[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries);

                Input = splitData[0].Select(w => w == '#').ToArray();
                Output = splitData[1].Select(w => w == '#').First();
            }

            public bool IsRight(params bool[] args)
            {
                return !args.Where((t, i) => t != Input[i]).Any();
            }
        }

        private class Cell
        {
            public int Position { get; set; }
            public bool IsPlant { get; set; }

            public Cell(int position, char plant)
            {
                Position = position;
                IsPlant = plant == '#';
            }

            private Cell(int position, bool isPlant)
            {
                Position = position;
                IsPlant = isPlant;
            }

            public Cell Copy()
            {
                return new Cell(Position, false);
            }
        }
    }
}

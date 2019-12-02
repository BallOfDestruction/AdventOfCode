using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2018.Tasks
{
    // Maybe in future this solution will be more ... comfortable
    /// <summary>
    /// https://adventofcode.com/2018/day/12
    /// </summary>
    public class DayTwelveTaskTwo : ITask
    {
        private const int INPUT_LENGTH = 5;
        private const long COUNT_GENERATION = 50000000000;
        private const int STEP = 10;

        public string Solve(string input)
        {
            var splitData = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var initData = splitData[0].Replace("initial state: ", "");

            var plantsLine = new PlantsLineContainer(initData);
            var rulesList = splitData.Skip(1).Select(w => new Rule(w)).ToList();

            long? lastDiff = null;
            long? lastValue = null;
            long? breakGeneration = null;

            for (var generation = 0L; generation < COUNT_GENERATION; generation++)
            {
                // The idea is that initially increases non-linear, but after increases become linear
                if (generation % STEP == 0)
                {
                    var answer = plantsLine.ToAnswer();

                    if (!lastValue.HasValue)
                    {
                        lastValue = answer;
                    }
                    else
                    {
                        var diff = answer - lastValue;

                        if (!lastDiff.HasValue)
                        {
                            lastDiff = diff;
                        }
                        else
                        {
                            lastValue = answer;
                            if (lastDiff == diff)
                            {
                                breakGeneration = generation;
                                break;
                            }
                            else
                            {
                                lastDiff = diff;
                            }
                        }
                    }
                }

                var nextGeneration = plantsLine.Position.Select(w => w.Copy()).ToList();

                //Console.WriteLine(container.Position.Aggregate("", (s, cell) => s + (cell.IsHavePlant ? "#" : ".")));

                for (var i = 2; i < plantsLine.Position.Count - 2; i++)
                {
                    var prePreCell = plantsLine.Position[i - 2];
                    var preCell = plantsLine.Position[i - 1];
                    var currentCell = plantsLine.Position[i];
                    var nextCell = plantsLine.Position[i + 1];
                    var nextNextCell = plantsLine.Position[i + 2];

                    var rightRules = rulesList.Where(w => w.IsApplicable(prePreCell.IsHavePlant, preCell.IsHavePlant,
                        currentCell.IsHavePlant, nextCell.IsHavePlant, nextNextCell.IsHavePlant)).ToList();

                    var currentRule = rightRules.FirstOrDefault();
                    if (currentRule != null)
                        nextGeneration[i].IsHavePlant = currentRule.Output;
                }

                var firstIsPlant = nextGeneration.Take(INPUT_LENGTH).TakeWhile(w => !w.IsHavePlant).ToList();
                if (firstIsPlant.Count < INPUT_LENGTH)
                {
                    var first = nextGeneration.First();
                    for (var i = 0; i < INPUT_LENGTH - firstIsPlant.Count; i++)
                    {
                        nextGeneration.Insert(0, new PlantCell(first.Position - i - 1, '.'));
                    }
                }

                var endIsPlant = nextGeneration.TakeLast(INPUT_LENGTH).ToList();
                endIsPlant.Reverse();
                endIsPlant = endIsPlant.TakeWhile(w => !w.IsHavePlant).ToList();
                if (endIsPlant.Count < INPUT_LENGTH)
                {
                    var last = nextGeneration.Last();
                    for (var i = 0; i < INPUT_LENGTH - endIsPlant.Count; i++)
                    {
                        nextGeneration.Add(new PlantCell(last.Position + i + 1, '.'));
                    }
                }

                plantsLine.Position = nextGeneration;
            }

            return (((COUNT_GENERATION - breakGeneration ?? 0) / STEP * lastDiff ?? 1) + lastValue ?? 0).ToString();
        }

        private class PlantsLineContainer
        {
            public List<PlantCell> Position { get; set; } = new List<PlantCell>();

            public PlantsLineContainer(string initData)
            {
                Position.Add(new PlantCell(-5, '.'));
                Position.Add(new PlantCell(-4, '.'));
                Position.Add(new PlantCell(-3, '.'));
                Position.Add(new PlantCell(-2, '.'));
                Position.Add(new PlantCell(-1, '.'));

                for (var index = 0; index < initData.Length; index++)
                {
                    Position.Add(new PlantCell(index, initData[index]));
                }

                Position.Add(new PlantCell(initData.Length, '.'));
                Position.Add(new PlantCell(initData.Length + 1, '.'));
                Position.Add(new PlantCell(initData.Length + 2, '.'));
                Position.Add(new PlantCell(initData.Length + 3, '.'));
                Position.Add(new PlantCell(initData.Length + 4, '.'));
            }

            public long ToAnswer()
            {
                return Position.Where(w => w.IsHavePlant).Sum(w => w.Position);
            }
        }

        private class Rule
        {
            public bool[] Input { get; }
            public bool Output { get; }

            public Rule(string data)
            {
                var splitData = data.Split(new[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries);

                Input = splitData[0].Select(w => w == '#').ToArray();
                Output = splitData[1].Select(w => w == '#').First();
            }

            public bool IsApplicable(params bool[] args)
            {
                return !args.Where((t, i) => t != Input[i]).Any();
            }
        }

        private class PlantCell
        {
            public int Position { get; }
            public bool IsHavePlant { get; set; }

            public PlantCell(int position, char plant)
            {
                Position = position;
                IsHavePlant = plant == '#';
            }

            private PlantCell(int position, bool isHavePlant)
            {
                Position = position;
                IsHavePlant = isHavePlant;
            }

            public PlantCell Copy()
            {
                return new PlantCell(Position, false);
            }
        }
    }
}

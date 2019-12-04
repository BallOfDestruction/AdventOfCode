using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
	/// https://adventofcode.com/2019/day/3
    /// </summary>
    public class DayThreeTaskTwo : ITask
    {
        private const int Size = 80000;

        public string Solve(string input)
        {
            var wires = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(w => w.Split(',').Select(c => new Command(c)).ToArray()).ToArray();

            var intersection = new List<long>();

            var field = new int[Size][];
            for (var i = 0; i < field.Length; i++)
            {
                field[i] = new int[Size];
            }

            var fieldIsEnable = new bool[Size][];
            for (var i = 0; i < fieldIsEnable.Length; i++)
            {
                fieldIsEnable[i] = new bool[Size];
            }

            var center = Size / 2;

            var secondWire = wires.Last();

            foreach (var wire in wires)
            {
                var currentX = center;
                var currentY = center;

                var path = 0;

                foreach (var command in wire)
                {
                    for (var i = 0; i < command.Distance; i++)
                    {
                        switch (command.Direction)
                        {
                            case Direction.Up:
                                currentY--;
                                break;
                            case Direction.Down:
                                currentY++;
                                break;
                            case Direction.Left:
                                currentX--;
                                break;
                            case Direction.Right:
                                currentX++;
                                break;
                        }

                        path++;

                        if (secondWire == wire)
                        {
                            if (fieldIsEnable[currentX][currentY])
                            {
                                intersection.Add(field[currentX][currentY] + path);
                            }
                        }
                        else
                        {
                            if (field[currentX][currentY] == default(char))
                            {
                                field[currentX][currentY] = path;
                                fieldIsEnable[currentX][currentY] = true;
                            }
                        }
                    }
                }
            }

            var answer = intersection.Min();

            return answer.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
	/// https://adventofcode.com/2019/day/3
    /// </summary>
    public class DayThreeTaskOne : ITask
    {
        private const int SIZE = 100000;

        public string Solve(string input)
        {
            var wires = input.Split(new[]{'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries).Select(w => w.Split(',').Select(c => new Command(c)).ToArray()).ToArray();

            var intersection = new List<(int x, int y)>();

            var field = new char[SIZE][];
            for (var i = 0; i < field.Length; i++)
            {
                field[i] = new char[SIZE];
            }

            var center = SIZE / 2;

            var secondWire = wires.Last();

            foreach (var wire in wires)
            {
                var currentX = center;
                var currentY = center;

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

                        if (secondWire == wire)
                        {
                            if (field[currentX][currentY] == '1')
                            {
                                intersection.Add((currentX, currentY));
                            }
                        }
                        else
                        {
                            field[currentX][currentY] = '1';
                        }
                    }
                }
            }

            var answer = intersection.Select(w => Math.Abs(w.x - center) + Math.Abs(w.y - center)).Min();

            return answer.ToString();
        }
    }

    public class Command
    {
        public Direction Direction { get; set; }

        public int Distance { get; set; }

        public Command(string command)
        {
            switch (command[0])
            {
                case 'R':
                    Direction = Direction.Right;
                    break;
                case 'L':
                    Direction = Direction.Left;
                    break;
                case 'U':
                    Direction = Direction.Up;
                    break;
                case 'D':
                    Direction = Direction.Down;
                    break;
            }

            Distance = int.Parse(command.Substring(1));
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}

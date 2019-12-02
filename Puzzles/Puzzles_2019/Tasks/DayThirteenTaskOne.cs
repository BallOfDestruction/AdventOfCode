using System;
using System.IO;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/12
    /// </summary>
    public class DayThirteenTaskOne : ITask
    {
        public string Solve(string input)
        {
            var allLines = input.Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var statedArrays = allLines.Select(w => w.Select(ToState).ToArray()).ToArray();

            var cards = allLines.SelectMany((line, index) =>
                    line.Select((item, itemIndex) => item is '<' || item is '>' || item is '^' || item is 'v'
                        ? new Cart(itemIndex, index, item)
                        : null))
                .Where(w => w != null).ToList();
            var tick = 0;
            while (true)
            {
                // don't matter order turns
                //cards = cards.OrderBy(w => w.Position.Y).ThenBy(w => w.Position.X).ToList();
                tick++;
                cards.ForEach(w => w.MoveTo(statedArrays));

                var file = new StreamWriter(File.Create($"file_{tick}.txt"));

                for (var index = 0; index < statedArrays.Length; index++)
                {
                    var allLine = statedArrays[index];
                    for (var i = 0; i < allLine.Length; i++)
                    {
                        var cellState = allLine[i];

                        var card = cards.FirstOrDefault(w => w.Position.X == i && w.Position.Y == index);
                        if (card != null)
                        {
                            file.Write("*");
                            continue;
                        }

                        switch (cellState)
                        {
                            case CellState.Horizontal:
                                file.Write("-");
                                break;

                            case CellState.Vertical:
                                file.Write("|");
                                break;

                            case CellState.Intersect:
                                file.Write("+");
                                break;

                            case CellState.LeftBottomAndRightTop:
                                file.Write("\\");
                                break;

                            case CellState.LeftTopAndRightBottom:
                                file.Write("/");
                                break;

                            case CellState.Nothing:
                                file.Write(" ");
                                break;
                        }
                    }

                    file.WriteLine();
                }
                file.Dispose();

                var crash = cards.GroupBy(w => w.Position).FirstOrDefault(w => w.Count() > 1);
                if (crash != null)
                    return $"{crash.Key.X},{crash.Key.Y}";
            }
        }

        private CellState ToState(char item)
        {
            switch (item)
            {
                case '-':
                case '<':
                case '>':
                    return CellState.Horizontal;
                case '|':
                case 'v':
                case '^':
                    return CellState.Vertical;
                case '+':
                    return CellState.Intersect;
                case '/':
                    return CellState.LeftTopAndRightBottom;
                case '\\':
                    return CellState.LeftBottomAndRightTop;
                case ' ':
                    return CellState.Nothing;
                default:
                    throw new NotImplementedException();
            }
        }

        public class Cart
        {
            public Position Position { get; set; }
            public Direction Direction { get; set; }
            public TurnType Turn { get; set; } = TurnType.Left;

            public Cart(int x, int y, char direction)
            {
                Position = new Position(x, y);

                switch (direction)
                {
                    case '<':
                        Direction = Direction.Left;
                        break;
                    case '>':
                        Direction = Direction.Right;
                        break;
                    case 'v':
                        Direction = Direction.Bottom;
                        break;
                    case '^':
                        Direction = Direction.Top;
                        break;
                }
            }

            public void MoveTo(CellState[][] statedArrays)
            {
                var position = Position;
                switch (Direction)
                {
                    case Direction.Bottom:
                        position.Y++;
                        break;

                    case Direction.Left:
                        position.X--;
                        break;

                    case Direction.Right:
                        position.X++;
                        break;

                    case Direction.Top:
                        position.Y--;
                        break;
                }

                Position = position;
                var currentState = statedArrays[Position.Y][Position.X];

                switch (currentState)
                {
                    // No turns
                    case CellState.Horizontal:
                        break;

                    // No turns
                    case CellState.Vertical:
                        break;

                    // Hard Turn!
                    case CellState.Intersect:
                        if (Turn == TurnType.Left)
                        {
                            if (Direction == Direction.Top)
                            {
                                Direction = Direction.Left;
                            }
                            else if (Direction == Direction.Bottom)
                            {
                                Direction = Direction.Right;
                            }
                            else if (Direction == Direction.Right)
                            {
                                Direction = Direction.Top;
                            }
                            else if (Direction == Direction.Left)
                            {
                                Direction = Direction.Bottom;
                            }
                        }
                        else if (Turn == TurnType.Right)
                        {
                            if (Direction == Direction.Top)
                            {
                                Direction = Direction.Right;
                            }
                            else if (Direction == Direction.Bottom)
                            {
                                Direction = Direction.Left;
                            }
                            else if (Direction == Direction.Right)
                            {
                                Direction = Direction.Bottom;
                            }
                            else if (Direction == Direction.Left)
                            {
                                Direction = Direction.Left;
                            }
                        }

                        ChangeTurnType();
                        break;

                    // Easy Turn!
                    case CellState.LeftTopAndRightBottom:
                        if (Direction == Direction.Top)
                        {
                            Direction = Direction.Right;
                        }
                        else if (Direction == Direction.Bottom)
                        {
                            Direction = Direction.Left;
                        }
                        else if (Direction == Direction.Right)
                        {
                            Direction = Direction.Top;
                        }
                        else if (Direction == Direction.Left)
                        {
                            Direction = Direction.Bottom;
                        }
                        break;

                    // Easy Turn!
                    case CellState.LeftBottomAndRightTop:
                        if (Direction == Direction.Top)
                        {
                            Direction = Direction.Left;
                        }
                        else if (Direction == Direction.Bottom)
                        {
                            Direction = Direction.Right;
                        }
                        else if (Direction == Direction.Right)
                        {
                            Direction = Direction.Bottom;
                        }
                        else if (Direction == Direction.Left)
                        {
                            Direction = Direction.Top;
                        }
                        break;
                }
            }

            private void ChangeTurnType()
            {
                Turn = (TurnType)((((int)Turn)+1) % 3);
            }
        }

        public struct Position
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public enum TurnType
        {
            Left,
            Forward,
            Right,
        }

        public enum Direction
        {
            Top,
            Bottom,
            Right,
            Left,
        }

        public enum CellState
        {
            Nothing,
            Vertical,
            Horizontal,
            Intersect,
            LeftTopAndRightBottom,
            LeftBottomAndRightTop,
        }
    }
}
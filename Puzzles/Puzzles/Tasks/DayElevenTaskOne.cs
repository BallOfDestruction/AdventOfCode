using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/11
    /// </summary>
    public class DayElevenTaskOne : ITask
    {
        public string Solve(string input)
        {
            var gridSerialNumber = int.Parse(input);

            var maxX = 300;
            var maxY = 300;
            var arrayCells = new Cell[maxX][];
            for (int i = 0; i < maxX; i++)
                arrayCells[i] = new Cell[maxY];

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    arrayCells[i][j] = new Cell(i, j, gridSerialNumber);
                }
            }

            GridInfo maxGridInfo = null;

            var listOfGridInfo = new List<GridInfo>();
            for (int i = 0; i < maxX - GridInfo.Wight; i++)
            {
                for (int j = 0; j < maxY - GridInfo.Height; j++)
                {
                    long amountOfAll = 0;

                    for (int ii = 0; ii < GridInfo.Wight; ii++)
                    {
                        for (int jj = 0; jj < GridInfo.Height; jj++)
                        {
                            amountOfAll += arrayCells[ii + i][jj + j].Power;
                        }
                    }

                    if (maxGridInfo == null || maxGridInfo.AmountPower < amountOfAll)
                        maxGridInfo = new GridInfo(i, j, amountOfAll);
                }
            }

            return $"{maxGridInfo.X},{maxGridInfo.Y}";
        }

        private class GridInfo
        {
            public const int Height = 3;
            public const int Wight = 3;

            public int X { get; }
            public int Y { get; }

            public long AmountPower { get; }

            public GridInfo(int x, int y, long amountPower)
            {
                X = x;
                Y = y;
                AmountPower = amountPower;
            }
        }

        private class Cell
        {
            public int X { get; }
            public int Y { get; }

            public int Power { get; set; }

            public Cell(int x, int y, int gridSerialNumber)
            {
                X = x;
                Y = y;

                Calculate(gridSerialNumber);
            }

            private void Calculate(int gridSerialNumber)
            {
                var rackId = X + 10;
                var powerLevel = rackId * Y;
                powerLevel += gridSerialNumber;
                powerLevel *= rackId;
                var hundred = powerLevel / 100 % 10;

                Power = hundred - 5;
            }
        }
    }
}

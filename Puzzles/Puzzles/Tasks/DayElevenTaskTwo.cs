using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/11
    /// </summary>
    public class DayElevenTaskTwo : ITask
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

            // Can be optimize by dynamic solution
            for (int size = 1; size <= maxX; size++)
            {
                for (int i = 0; i < maxX - size; i++)
                {
                    for (int j = 0; j < maxY - size; j++)
                    {
                        long amountOfAll = 0;

                        for (int ii = 0; ii < size; ii++)
                        {
                            for (int jj = 0; jj < size; jj++)
                            {
                                amountOfAll += arrayCells[ii + i][jj + j].Power;
                            }
                        }

                        if (maxGridInfo == null || maxGridInfo.AmountPower < amountOfAll)
                            maxGridInfo = new GridInfo(i, j, size, amountOfAll);
                    }
                }
            }

            return $"{maxGridInfo.X},{maxGridInfo.Y},{maxGridInfo.Size}";
        }

        private class GridInfo
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Size { get; set; }

            public long AmountPower { get; set; }

            public GridInfo(int x, int y, int size, long amountPower)
            {
                X = x;
                Y = y;
                Size = size;
                AmountPower = amountPower;
            }
        }

        private class Cell
        {
            public int X { get; set; }
            public int Y { get; set; }

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

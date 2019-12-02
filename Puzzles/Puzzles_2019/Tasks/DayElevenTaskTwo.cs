using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/11
    /// </summary>
    public class DayElevenTaskTwo : ITask
    {
        private const int MAX_X = 300;
        private const int MAX_Y = 300;

        public string Solve(string input)
        {
            var gridSerialNumber = int.Parse(input);

            var arrayCells = InitArrayCell(MAX_X, MAX_Y, gridSerialNumber);

            var answer = FindMaxSquare(arrayCells);

            return $"{answer.X},{answer.Y},{answer.Size}";
        }

        private GridInfo FindMaxSquare(Cell[][] arrayCells)
        {
            GridInfo maxGridInfo = null;

            // Can be optimize by dynamic solution
            for (var size = 1; size <= MAX_X; size++)
            {
                for (var x = 0; x < MAX_X - size; x++)
                {
                    for (var y = 0; y < MAX_Y - size; y++)
                    {
                        long amountOfAll = 0;

                        for (var xx = 0; xx < size; xx++)
                        {
                            for (var yy = 0; yy < size; yy++)
                            {
                                amountOfAll += arrayCells[xx + x][yy + y].Power;
                            }
                        }

                        if (maxGridInfo == null || maxGridInfo.AmountPower < amountOfAll)
                            maxGridInfo = new GridInfo(x, y, size, amountOfAll);
                    }
                }
            }

            return maxGridInfo;
        }

        private static Cell[][] InitArrayCell(int maxX, int maxY, int gridSerialNumber)
        {
            var arrayCells = new Cell[maxX][];
            for (var i = 0; i < maxX; i++)
                arrayCells[i] = new Cell[maxY];

            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    arrayCells[x][y] = new Cell(x, y, gridSerialNumber);
                }
            }

            return arrayCells;
        }

        private class GridInfo
        {
            public int X { get; }
            public int Y { get; }
            public int Size { get; }

            public long AmountPower { get; }

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

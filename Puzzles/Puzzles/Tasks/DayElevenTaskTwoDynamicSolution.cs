namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/11
    /// </summary>
    public class DayElevenTaskTwoDynamicSolution : ITask
    {
        private const int MaxSize = 300;

        public string Solve(string input)
        {
            var gridSerialNumber = int.Parse(input);

            var arrayCells = InitArrayCell(gridSerialNumber);

            var answer = FindMaxSquare(arrayCells);

            return $"{answer.X},{answer.Y},{answer.Size}";
        }

        private Cell FindMaxSquare(long[][][] arrayCells)
        {
            Cell maxGridInfo = null;

            // Can be optimize by dynamic solution
            for (var size = 1; size <= MaxSize; size++)
            {
                var maxSize = MaxSize - size;
                for (var x = 0; x < maxSize; x++)
                {
                    for (var y = 0; y < maxSize; y++)
                    {
                        var amountOfAll = arrayCells[size - 1][x][y];

                        var maxX = x + size;
                        var maxY = y + size;

                        for (var index = 0; index < maxX; index++)
                            amountOfAll += arrayCells[0][index][maxY];

                        for (var index = 0; index < maxY; index++)
                            amountOfAll += arrayCells[0][maxX][index];

                        amountOfAll += arrayCells[0][maxX][maxY];
                        arrayCells[size][x][y] = amountOfAll;

                        if (maxGridInfo == null || maxGridInfo.Power <= amountOfAll)
                            maxGridInfo = new Cell(x, y, size, amountOfAll);
                    }
                }
            }

            return maxGridInfo;
        }

        private static long[][][] InitArrayCell(int gridSerialNumber)
        {
            var arrayCells = new long[MaxSize][][];
            for (var size = 0; size < MaxSize; size++)
                arrayCells[size] = new long[MaxSize][];

            for (var size = 0; size < MaxSize; size++)
            {
                var localMaxSize = MaxSize - size;
                for (var x = 0; x < localMaxSize; x++)
                {
                    arrayCells[size][x] = new long[localMaxSize];
                }
            }

            for (var size = 0; size < MaxSize; size++)
            {
                var localMaxSize = MaxSize - size;
                for (var x = 0; x < localMaxSize; x++)
                {
                    for (var y = 0; x < localMaxSize; x++)
                    {
                        if (size == 0)
                            arrayCells[size][x][y] = Calculate(x, y, gridSerialNumber);
                    }
                }
            }

            return arrayCells;
        }

        private static long Calculate(int x, int y, int gridSerialNumber)
        {
            var rackId = x + 10;
            var powerLevel = rackId * y;
            powerLevel += gridSerialNumber;
            powerLevel *= rackId;
            var hundred = powerLevel / 100 % 10;

            return hundred - 5;
        }

        private class Cell
        {
            public int X { get; }
            public int Y { get; }
            public int Size { get; }
            public long Power { get; }

            public Cell(int x, int y, int size, long power)
            {
                X = x;
                Y = y;
                Size = size;
                Power = power;
            }
        }
    }
}

using System;
using System.Linq;

namespace Puzzles.Tasks
{
    public class DayThreeTaskOne : ITask
    {
        public string Solve(string input)
        {
            var allStrings = input.Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
            var allIntData = allStrings.Select(w =>
                w.Split(new[] {'#', '@', ',', ':', 'x', ' '}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                    .ToArray());

            var listData = allIntData.Select(w => new Coordinates(w[1], w[2], w[3], w[4])).ToList();

            var maxWidth = listData.Select(w => w.X + w.Width).Max() + 1;
            var maxHeight = listData.Select(w => w.Y + w.Height).Max() + 1;

            var array = new int[maxWidth, maxHeight];

            listData.ForEach(w => Increment(array, w));

            var answer = GetAnswer(array, maxWidth, maxHeight);

            return answer.ToString();
        }

        private int GetAnswer(int[,] array, int width, int height)
        {
            var answer = 0;
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (array[i, j] > 1)
                        answer++;
                }
            }

            return answer;
        }

        private void Increment(int[,] array, Coordinates coordinates)
        {
            for (var i = coordinates.X; i < coordinates.X + coordinates.Width; i++)
            {
                for (var j = coordinates.Y; j < coordinates.Y + coordinates.Height; j++)
                {
                    array[i, j]++;
                }
            }
        }

        private class Coordinates
        {
            public int X { get; }
            public int Y { get; }

            public int Width { get; }
            public int Height { get; }

            public Coordinates(int x, int y, int width, int height)
            {
                X = x;
                Y = y;
                Width = width;
                Height = height;
            }
        }
    }
}

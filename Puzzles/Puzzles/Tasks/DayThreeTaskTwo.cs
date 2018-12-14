using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    public class DayThreeTaskTwo : ITask
    {
        public string Solve(string input)
        {
            var allStrings = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var allIntData = allStrings.Select(w =>
                w.Split(new[] { '#', '@', ',', ':', 'x', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                    .ToArray());

            var listData = allIntData.Select(w => new Coordinates(w[0], w[1], w[2], w[3], w[4])).ToList();

            var maxWidth = listData.Select(w => w.X + w.Width).Max() + 1;
            var maxHeight = listData.Select(w => w.Y + w.Height).Max() + 1;

            var array = new int[maxWidth, maxHeight];

            listData.ForEach(w => Increment(array, w));

            var answer = GetAnswer(array, listData);

            return answer.ToString();
        }

        private int GetAnswer(int[,] array, List<Coordinates> items)
        {
            foreach (var coordinate in items)
            {
                if (Check(coordinate, array))
                    return coordinate.Id;
            }

            return 0;
        }

        private bool Check(Coordinates coordinate, int[,] array)
        {
            for (var i = coordinate.X; i < coordinate.X + coordinate.Width; i++)
            {
                for (var j = coordinate.Y; j < coordinate.Y + coordinate.Height; j++)
                {
                    if (array[i, j] > 1)
                        return false;
                }
            }

            return true;
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
            public int Id { get; set; }

            public int X { get; }
            public int Y { get; }

            public int Width { get; }
            public int Height { get; }

            public Coordinates(int id, int x, int y, int width, int height)
            {
                Id = id;
                X = x;
                Y = y;
                Width = width;
                Height = height;
            }
        }
    }
}

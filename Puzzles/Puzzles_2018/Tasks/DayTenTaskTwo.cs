using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2018.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/10
    /// </summary>
    public class DayTenTaskTwo : ITask
    {
        public string Solve(string input)
        {
            var data = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(w => new Vector(w)).ToList();

            var historySize = new List<Size>();

            Size lastSize = null;

            // Calculate near 100000 variables and find with min sizes
            while (true)
            {
                foreach (var vector in data)
                {
                    vector.X += vector.VelocityX;
                    vector.Y += vector.VelocityY;
                }

                var xses = data.Select(w => w.X).ToList();
                var maxX = xses.Max();
                var minX = xses.Min();
                var offsetX = maxX - minX + 1;

                var yses = data.Select(w => w.Y).ToList();
                var maxY = yses.Max();
                var minY = yses.Min();
                var offsetY = maxY - minY + 1;

                var size = new Size(offsetX, offsetY);
                historySize.Add(size);

                if (lastSize != null && lastSize.CompareTo(size) < 0)
                    break;

                lastSize = size;
            }

            var minSize = historySize.Min();
            var indexMin = historySize.IndexOf(minSize);

            return (indexMin + 1).ToString();
        }

        private class Size : IComparable<Size>
        {
            public int X { get; }
            public int Y { get; }

            public Size(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int CompareTo(Size other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return (X + Y).CompareTo(other.X + other.Y);
            }
        }

        private class Vector
        {
            public int X { get; set; }
            public int Y { get; set; }

            public int VelocityX { get; }
            public int VelocityY { get; }

            public Vector(string input)
            {
                var split = input.Split(new[] { '<', '>', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                X = int.Parse(split[1]);
                Y = int.Parse(split[2]);

                VelocityX = int.Parse(split[4]);
                VelocityY = int.Parse(split[5]);
            }
        }
    }
}

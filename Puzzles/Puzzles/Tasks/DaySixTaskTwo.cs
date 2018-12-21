using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/6
    /// </summary>
    public class DaySixTaskTwo : ITask
    {
        public string Solve(string input)
        {
            var coordinates = input.Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries)
                .Select((w, index) => new Coordinate(w, index))
                .ToList();

            var maxY = coordinates.Select(w => w.Y).Max();
            var maxX = coordinates.Select(w => w.X).Max();

            // Init coordinate arrays
            var coordinateDistance = new CoordinateDistanceData[maxX][];
            for (var i = 0; i < maxX; i++)
                coordinateDistance[i] = new CoordinateDistanceData[maxY];

            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    coordinateDistance[x][y] =
                        new CoordinateDistanceData(new Coordinate(x, y));
                }
            }

            // Calculate all distance and their sum
            foreach (var coordinate in coordinates)
            {
                for (var x = 0; x < maxX; x++)
                {
                    for (var y = 0; y < maxY; y++)
                    {
                        var item = coordinateDistance[x][y];
                        item.TrySetData(coordinate);
                    }
                }
            }

            var maxDistance = 10000;
            var count = 0L;

            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    var item = coordinateDistance[x][y].Count;
                    if (item < maxDistance)
                        count++;
                }
            }

            return count.ToString();
        }

        private class CoordinateDistanceData
        {
            public Coordinate Coordinate { get; }

            public long Count { get; private set; }

            public CoordinateDistanceData(Coordinate coordinate)
            {
                Coordinate = coordinate;
            }

            public void TrySetData(Coordinate coordinate)
            {
                Count += coordinate.DistanceTo(Coordinate);
            }
        }

        private class Coordinate
        {
            public int X { get; }
            public int Y { get; }

            public int Index { get; }

            public Coordinate(string input, int index)
            {
                var data = input.Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                    .ToArray();
                X = data[0];
                Y = data[1];

                Index = index;
            }

            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int DistanceTo(Coordinate destination)
            {
                return Math.Abs(X - destination.X) + Math.Abs(Y - destination.Y);
            }
        }
    }
}

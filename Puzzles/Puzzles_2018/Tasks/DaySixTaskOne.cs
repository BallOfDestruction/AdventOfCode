using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2018.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/6
    /// </summary>
    public class DaySixTaskOne : ITask
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
                    coordinateDistance[x][y] = new CoordinateDistanceData(new Coordinate(x, y));
                }
            }

            // Calculate all distance
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

            // Calculate nearest data
            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    coordinateDistance[x][y].CalculateNearestData();
                }
            }

            // amount of area per coordinate
            var dictionary = new Dictionary<Coordinate, int>();

            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    var nearestCoordinate = coordinateDistance[x][y].NearestData?.Coordinate;
                    if (nearestCoordinate == null) continue;

                    if (dictionary.ContainsKey(nearestCoordinate))
                        dictionary[nearestCoordinate]++;
                    else
                        dictionary.Add(nearestCoordinate, 1);
                }
            }

            // Remove all coordinates at edges
            for (var x = 0; x < maxX; x++)
            {
                var item = coordinateDistance[x][0].NearestData?.Coordinate;
                if(item != null)
                    dictionary.Remove(item);
            }

            for (var x = 0; x < maxX; x++)
            {
                var item = coordinateDistance[x][maxY - 1].NearestData?.Coordinate;
                if (item != null)
                    dictionary.Remove(item);
            }

            for (var y = 0; y < maxY; y++)
            {
                var item = coordinateDistance[0][y].NearestData?.Coordinate;
                if (item != null)
                    dictionary.Remove(item);
            }

            for (var y = 0; y < maxY; y++)
            {
                var item = coordinateDistance[maxX - 1][y].NearestData?.Coordinate;
                if (item != null)
                    dictionary.Remove(item);
            }

            var answer = dictionary.Select(w => w.Value).Max();

            return answer.ToString();
        }

        // ReSharper disable once UnusedMember.Local
        private void ShowData(CoordinateDistanceData[][] data, int maxX, int maxY)
        {
            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    var item = data[x][y];
                    Console.Write(item.NearestData?.Coordinate?.Index.ToString("00") ?? "..");
                    Console.Write("  ");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private class DistanceData
        {
            public Coordinate Coordinate { get; }

            public int Distance { get; }

            public DistanceData(Coordinate coordinate, int distance)
            {
                Coordinate = coordinate;
                Distance = distance;
            }
        }

        private class CoordinateDistanceData
        {
            public Coordinate Coordinate { get; }
            public List<DistanceData> Coordinates { get; } = new List<DistanceData>();

            public DistanceData NearestData { get; private set; }

            public CoordinateDistanceData(Coordinate coordinate)
            {
                Coordinate = coordinate;
            }

            public void TrySetData(Coordinate coordinate)
            {
                Coordinates.Add(new DistanceData(coordinate, coordinate.DistanceTo(Coordinate)));
            }

            public void CalculateNearestData()
            {
                var data = Coordinates.OrderBy(w => w.Distance);
                var first = data.First();
                var countSame = Coordinates.Count(w => w.Distance == first.Distance);
                NearestData = countSame > 1 ? null : first;
            }
        }

        private class Coordinate
        {
            public int X { get; }
            public int Y { get; }

            public int Index { get; }

            public Coordinate(string input, int index)
            {
                var data = input.Split(new []{' ', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
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
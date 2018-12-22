using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/8
    /// </summary>
    public class DayEightTaskOne : ITask 
    {
        public string Solve(string input)
        {
            var data = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            var listNodes = new List<Node>();

            FillNode(data, listNodes);

            var answer = listNodes.Select(w => w.Metadata.Sum()).Sum();

            return answer.ToString();
        }

        private void FillNode(List<int> data, List<Node> listNodes)
        {
            var childCount = data[0];
            var metadataCount = data[1];

            data.RemoveRange(0, 2);

            for (var childIndex = 0; childIndex < childCount; childIndex++)
            {
                FillNode(data, listNodes);
            }

            var metadataItems = data.Take(metadataCount).ToList();
            data.RemoveRange(0, metadataCount);
            listNodes.Add(new Node(metadataItems));
        }

        private class Node
        {
            public List<int> Metadata { get; }

            public Node(List<int> metadata)
            {
                Metadata = metadata;
            }
        }
    }
}

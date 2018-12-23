using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/8
    /// </summary>
    public class DayEightTaskTwo : ITask
    {
        public string Solve(string input)
        {
            var data = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            var listNodes = new List<Node>();

            var rootNode = FillNode(data, listNodes);

            CalculateNode(rootNode);

            return rootNode.Worth.ToString();
        }

        private void CalculateNode(Node node)
        {
            if (node.ChildNodes.Any())
            {
                foreach (var nodeChildNode in node.ChildNodes)
                {
                    CalculateNode(nodeChildNode);
                }

                long worth = 0;
                foreach (var metadata in node.Metadata)
                {
                    if (metadata <= node.ChildNodes.Count)
                    {
                        worth += node.ChildNodes[metadata - 1].Worth;
                    }
                }

                node.Worth = worth;
            }
            else
            {
                node.Worth = node.Metadata.Sum();
            }
        }

        private Node FillNode(List<int> data, List<Node> listNodes)
        {
            var childCount = data[0];
            var metadataCount = data[1];

            data.RemoveRange(0, 2);

            var childNodes = new List<Node>();

            for (var childIndex = 0; childIndex < childCount; childIndex++)
            {
                childNodes.Add(FillNode(data, listNodes));
            }

            var metadataItems = data.Take(metadataCount).ToList();
            data.RemoveRange(0, metadataCount);

            var currentNode = new Node(metadataItems, childNodes);
            listNodes.Add(currentNode);

            return currentNode;
        }

        private class Node
        {
            public long Worth { get; set; }
            public List<int> Metadata { get; }
            public List<Node> ChildNodes { get; }

            public Node(List<int> metadata, List<Node> childNodes)
            {
                Metadata = metadata;
                ChildNodes = childNodes;
            }
        }
    }
}

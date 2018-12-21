using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/7
    /// </summary>
    public class DaySevenTaskOne : ITask
    {
        public string Solve(string input)
        {
            var linkData = input.Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries).Select(w => new LinkData(w)).ToList();

            var linkDictionary = linkData.Select(w => w.From)
                .Union(linkData.Select(w => w.To)).Distinct()
                .ToDictionary(w => w, w => new List<char>());

            foreach (var data in linkData)
            {
                linkDictionary[data.To].Add(data.From);
            }

            var output = "";

            while (linkDictionary.Any())
            {
                var (key, _) = linkDictionary.Where(w => !w.Value.Any()).OrderBy(w => w.Key).First();
                output += key;
                linkDictionary.Remove(key);

                foreach (var (_, value) in linkDictionary)
                {
                    if (value.Contains(key))
                        value.Remove(key);
                }
            }

            return output;
        }

        private class LinkData
        {
            public char From { get; }  
            public char To { get; }

            public LinkData(string input)
            {
                // Step C must be finished before step A can begin.
                From = input[5];
                To = input[36];
            }
        }
    }
}

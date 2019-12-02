using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Puzzles_2018.Tasks
{
    /// <summary>
	/// https://adventofcode.com/2018/day/7
    /// </summary>
    public class DaySevenTaskTwo : ITask
    {
        public string Solve(string input)
        {
            var linkData = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(w => new LinkData(w)).ToList();

            var linkDictionary = linkData.Select(w => w.From)
                .Union(linkData.Select(w => w.To)).Distinct()
                .ToDictionary(w => w, w => new List<char>());

            foreach (var data in linkData)
            {
                linkDictionary[data.To].Add(data.From);
            }

            var aCharInt = 'A' - 1;
            var workers = new List<WorkerData>();
            var maxWorkerCount = 5;
            var elapsedTime = 0;

            while (linkDictionary.Any() || workers.Any())
            {
                workers.ForEach(w => w.NeedTime--);

                var needRemove = workers.Where(w => w.NeedTime == 0).ToList();

                foreach (var workerData in needRemove)
                {

                    foreach (var (_, value) in linkDictionary)
                    {
                        if (value.Contains(workerData.Key))
                            value.Remove(workerData.Key);
                    }

                    workers.Remove(workerData);
                }

                if(!linkDictionary.Any() && !workers.Any()) break;

                elapsedTime++;

                if (workers.Count < maxWorkerCount)
                {
                    var nextItems = linkDictionary.Where(w => !w.Value.Any()).ToList();
                    if (nextItems.Any())
                    {
                        nextItems = nextItems.OrderBy(w => w.Key).Take(maxWorkerCount - workers.Count).ToList();
                        foreach (var keyValuePair in nextItems)
                        {
                            workers.Add(new WorkerData(keyValuePair.Key - aCharInt + 60, keyValuePair.Key));
                            linkDictionary.Remove(keyValuePair.Key);
                        }
                    }
                }
            }

            return elapsedTime.ToString();
        }

        private class WorkerData
        {
            public int NeedTime { get; set; }
            public char Key { get; }

            public WorkerData(int needTime, char key)
            {
                NeedTime = needTime;
                Key = key;
            }
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

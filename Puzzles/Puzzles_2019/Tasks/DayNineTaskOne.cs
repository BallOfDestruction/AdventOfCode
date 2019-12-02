using System;
using System.Linq;
using Shared;

namespace Puzzles_2019.Tasks
{
    /// <summary>
    /// https://adventofcode.com/2018/day/9
    /// </summary>
    public class DayNineTaskOne : ITask
    {
        public string Solve(string input)
        {
            var data = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var playersCount = int.Parse(data[0]);
            var worthLastMarble = int.Parse(data[6]);

            var marbles = Enumerable.Range(1, worthLastMarble).Select(w => new Marble(w)).ToArray();
            var players = Enumerable.Range(1, playersCount).Select(w => new Player()).ToList();

            for (var i = 0; i < players.Count - 1; i++)
            {
                var player = players[i];
                var nextPlayer = players[i + 1];
                player.NextPlayer = nextPlayer;
            }
            players[playersCount - 1].NextPlayer = players[0];

            var firstMarble = new Marble(0);
            firstMarble.NextMarble = firstMarble;
            var currentPlayer = players[0];
            var currentMarble = firstMarble;

            for (var j = 0; j < worthLastMarble; j++)
            {
                var marble = marbles[j];

                if (marble.Worth % 23 == 0)
                {
                    currentPlayer.Score += marble.Worth;

                    var revertMarble = currentMarble;

                    for (var i = 0; i < 7; i++)
                    {
                        revertMarble = revertMarble.PreviousMarble;
                    }

                    // Remove from two-way link list
                    currentMarble = revertMarble.NextMarble;
                    currentPlayer.Score += revertMarble.Worth;

                    currentMarble.PreviousMarble = revertMarble.PreviousMarble;
                    revertMarble.PreviousMarble.NextMarble = currentMarble;
                }
                else
                {
                    // Insert in two-way link list
                    var afterMarble = currentMarble.NextMarble;
                    var nextAfterMarble = afterMarble.NextMarble;

                    afterMarble.NextMarble = marble;

                    marble.NextMarble = nextAfterMarble;
                    marble.PreviousMarble = afterMarble;

                    nextAfterMarble.PreviousMarble = marble;

                    currentMarble = marble;
                }

                currentPlayer = currentPlayer.NextPlayer;
            }

            return players.Select(w => w.Score).Max().ToString();
        }

        public class Marble
        {
            public int Worth { get; set; }
            public Marble NextMarble { get; set; }
            public Marble PreviousMarble { get; set; }

            public Marble(int worth)
            {
                Worth = worth;
            }
        }

        public class Player
        {
            public long Score { get; set; }
            public Player NextPlayer { get; set; }
        }
    }
}

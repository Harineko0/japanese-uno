using System;
using System.Collections.Generic;
using System.Linq;

namespace JapaneseUno
{
    public class TableAnalyzer
    {
        public int trialsSum;
        public int firstWinTimesSum;
        
        public void Analyze(List<Table> tables)
        {
            int trials = tables.Count;
            int firstWinTimes = 0;
            var firstCardWinTimes = new int[tables[0].History[0].Players[0].Cards.Count];
            var cardTimes = new int[tables[0].History[0].Players[0].Cards.Count];

            for (int i = 0; i < trials; i++)
            {
                var table = tables[i];

                if (table.WinNumber == 0)
                {
                    firstWinTimes++;

                    firstCardWinTimes[table.History[1].Layout.Peek().Number - 1]++;
                }

                cardTimes[table.History[1].Layout.Peek().Number - 1]++;
            }
            
            Console.WriteLine("Trials: " + trials 
                                         + "\nFirst Win Times: " + firstWinTimes 
                                         + "\nCard Win Probabilities: " + string.Join(", ", 
                                             firstCardWinTimes.Select((times, card) =>
                                                 "\n" + (card + 3) + ": " +
                                                 times + " / " + cardTimes[card] + " = " + Math.Round((double) times / cardTimes[card] * 100, 1))));

            trialsSum += trials;
            firstWinTimesSum += firstWinTimes;
        }
    }
}
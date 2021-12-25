using System;
using System.Collections.Generic;

namespace JapaneseUno
{
    public class TableController
    {
        private List<Table> tables = new List<Table>();
        
        public List<Table> Start(GameConfig config)
        {
            List<Player> players = new List<Player>();
            List<Card> cards = new List<Card>();

            for (int i = 1; i <= config.maxCard; i++)
            {
                cards.Add(new Card(i));
            }

            for (int i = 0; i < config.playerNumber; i++)
            {
                players.Add(new Player(new List<Card>(cards)));
            }
            
            Table table = new Table(players);
            
            Next(table);
            return tables;
        }
        
        public void Next(Table table)
        {
            if (table.IsEnd())
            {
                Result(table);
                return;
            }

            if (table.CanPass())
            {
                Table nextTable = table.Clone();
                nextTable.Pass();
                Console.WriteLine("-- Pass ----------------------------\n" + nextTable);
                Next(nextTable);
            }
            else
            {
                List<Card> cards = new List<Card>(table.Playing.Cards);
                foreach (var card in cards)
                {
                    if (table.CanPlayCard(card))
                    {
                        Table nextTable = table.Clone();
                        nextTable.PlayCard(card);

                        Console.WriteLine("-- Play ----------------------------\n" + nextTable);
                        // Console.WriteLine("======================\n" + nextTable);
                        // Console.WriteLine(IsDebug(nextTable));
                        if (IsDebug(nextTable))
                        {
                            // Console.WriteLine("------------------------------\n" + nextTable);
                            Next(nextTable);
                        }
                    }
                }   
            }
        }

        private void Result(Table table)
        {
            tables.Add(table);
        }

        private bool IsDebug(Table table)
        {
            var query = new []{1, 2, 4, 2};
            for (int i = 0; i < table.History.Count; i++)
            {
                var history = table.History[i];
                if (history.Layout.Count > 0)
                {
                    int layout = history.Layout.Peek().Number;
                    // Console.WriteLine("[IsDebug] History " + i + " -> Layout = " + layout + ", Query = " + query[i - 1]);
                    if (i < query.Length && query[i - 1] != layout)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
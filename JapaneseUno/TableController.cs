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
            // Next(table);
            SplitProcess(table);

            // Table table2 = table.Clone();
            // table2.PlayCard(new Card(9));
            // Next(table2);
            
            return tables;
        }
        
        private void Next(Table table)
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
                // Console.WriteLine("-- Pass ----------------------------\n" + nextTable);
                Next(nextTable);
            }
            else
            {
                List<Card> cards = new List<Card>(table.Playing.Cards);

                for (int i = 0; i < cards.Count; i++)
                {
                    var card = cards[i];
                    
                    if (table.CanPlayCard(card))
                    {
                        Table nextTable = table.Clone();
                        nextTable.PlayCard(card);
                        
                        // Console.WriteLine("-- Play ----------------------------\n" + nextTable);
                
                        Next(nextTable);
                    }
                }   
            }
        }

        private void Result(Table table)
        {
            tables.Add(table);
        }

        private void SplitProcess(Table _table)
        {
            for (int cardNum = 1; cardNum <= _table.Playing.Cards.Count; cardNum++)
            {
                var analyzer = new TableAnalyzer();

                var firstCard = new Card(cardNum);
                Table table = _table.Clone();
                table.PlayCard(firstCard);

                if (table.CanPass())
                {
                    Table nextTable = table.Clone();
                    nextTable.Pass();

                    List<Card> cards = new List<Card>(nextTable.Playing.Cards);

                    for (int i = 0; i < cards.Count; i++)
                    {
                        var card = cards[i];

                        if (nextTable.CanPlayCard(card))
                        {
                            Table afterNextTable = nextTable.Clone();
                            afterNextTable.PlayCard(card);
                            Next(afterNextTable);
                        }

                        if (tables.Count > 0)
                        {
                            Console.WriteLine(firstCard.Number + "_" + (i + 1));
                            analyzer.Analyze(tables);
                            tables.Clear();
                        }
                    }

                }
                else
                {
                    List<Card> cards = new List<Card>(table.Playing.Cards);

                    for (int i = 0; i < cards.Count; i++)
                    {
                        var card = cards[i];

                        if (table.CanPlayCard(card))
                        {
                            Table nextTable = table.Clone();
                            nextTable.PlayCard(card);
                            Next(nextTable);
                        }

                        if (tables.Count > 0)
                        {
                            Console.WriteLine(firstCard.Number + "_" + (i + 1));
                            analyzer.Analyze(tables);
                            tables.Clear();
                        }
                    }
                }

                Console.WriteLine("TrialsSum: " + analyzer.trialsSum + ", FirstWinSum: " + analyzer.firstWinTimesSum + "\n---------------------------------");
            }
        }

        private bool IsDebug(Table table)
        {
            var query = new []{3};
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
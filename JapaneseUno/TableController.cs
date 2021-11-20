using System;
using System.Collections.Generic;

namespace JapaneseUno
{
    class TableController
    {
        public void Start(GameConfig config)
        {
            Table table = new Table();
            List<Card> cards = new List<Card>();

            for (int i = 1; i <= config.maxCard; i++)
            {
                cards.Add(new Card(i));
            }

            for (int i = 0; i < config.playerNumber; i++)
            {
                table.AddPlayer(new Player(new List<Card>(cards)));
            }
            
            Next(table);
        }
        
        public void Next(Table table)
        {
            Console.WriteLine("------------------------------\n" + table);
            
            if (table.IsEnd())
            {
                Result(table);
                return;
            }

            if (table.Passable())
            {
                Console.WriteLine("***** Pass!");
                // パス
                Table nextTable = table.Clone();
                nextTable.Pass();
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
                        Next(nextTable);
                    }
                }   
            }
        }

        private void Result(Table table)
        {
            Console.WriteLine("------------------------------\nGame Finished.");
        }
    }
}
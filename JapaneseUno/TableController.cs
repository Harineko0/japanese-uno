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

            if (table.IsPassable())
            {
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
            tables.Add(table);
        }
    }
}
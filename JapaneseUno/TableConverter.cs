using System.Collections.Generic;
using System.Linq;

namespace JapaneseUno
{
    public class TableConverter
    {
        public List<Dictionary<string, string>> ToCSV(List<Table> tables)
        {
            List<Dictionary<string, string>> csvTables = new List<Dictionary<string, string>>();
            
            foreach (var table in tables)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Win", (table.WinNumber + 1).ToString());
                
                foreach (var history in table.History)
                {
                    string cardsString = "";
                    
                    for (int i = 0; i < history.Players.Count; i++)
                    {
                        var player = history.Players[i];
                        var cards = player.Cards.Select(card => card.Number + 2);
                        cardsString += string.Join(", ", cards);
                        if (i < history.Players.Count - 1)
                        {
                            cardsString += " - ";
                        }
                    }

                    cardsString.TrimEnd();
                    dic.Add("Turn - " + history.Turn, cardsString);
                }
                
                csvTables.Add(dic);
            }

            return csvTables;
        }
    }
}
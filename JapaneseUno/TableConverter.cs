using System;
using System.Collections.Generic;
using System.Text;

namespace JapaneseUno
{
    public class CsvOptions
    {
        public int HistoryLimit = -1;
    }
    
    public class TableConverter
    {
        public List<Dictionary<string, string>> ToCSV(List<Table> tables, CsvOptions options)
        {
            List<Dictionary<string, string>> csvTables = new List<Dictionary<string, string>>();
            
            for (int i = 0; i < tables.Count; i++)
            {
                var table = tables[i];
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Win", (table.WinNumber + 1).ToString());
                // dic.Add("Layout", "");
                // List<Card> layouts = new List<Card>();

                int historyLimit = options.HistoryLimit == -1 ? table.History.Count : options.HistoryLimit;
                
                for (int j = 0; j < historyLimit; j++)
                {
                    var history = table.History[j];
                    var cardsString = new StringBuilder();
                    if (history.Layout.Count != 0)
                    {
                        var currentLayout = history.Layout.Peek();
                        // layouts.Add(currentLayout);
                        cardsString.Append("[").Append(currentLayout).Append("]\n");
                    }
                    
                    for (int k = 0; k < history.Players.Count; k++)
                    {
                        var player = history.Players[k];
                        cardsString.Append(string.Join(", ", player.Cards));
                        if (k < history.Players.Count - 1)
                        {
                            cardsString.Append("\n");
                        }
                    }

                    var key = "Turn - " + history.Turn;
                    if (dic.ContainsKey(key))
                    {
                        dic[key] = cardsString.ToString().TrimEnd();
                    }
                    else
                    {
                        dic.Add("Turn - " + history.Turn, cardsString.ToString().TrimEnd());
                    }
                }

                // dic["Layout"] = string.Join(", ", layouts);
                
                csvTables.Add(dic);
            }

            return csvTables;
        }
    }
}
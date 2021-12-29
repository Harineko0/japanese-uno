using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace JapaneseUno
{
    public class FileExporter
    {
        public void WriteDictionary(string path, List<Dictionary<string, string>> dictionaries, bool append)
        {
            var writer = new StreamWriter(path, append);
            
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                var keys = dictionaries.Select(x => x.Keys.ToList()).ToList();
                keys.Sort((a, b) => b.Count - a.Count);
                var headings = new List<string>(keys.First());
                
                if (!append)
                {
                    csv.WriteField("Order");
                    foreach (var heading in headings)
                    {
                        csv.WriteField(heading);
                    }
                }

                csv.NextRecord();

                int order = 1;
                
                foreach (var item in dictionaries)
                {
                    csv.WriteField(order.ToString());
                    order += 1;
                    
                    foreach (var heading in headings)
                    {
                        if (item.ContainsKey(heading))
                        {
                            csv.WriteField(item[heading]);
                        }
                    }

                    csv.NextRecord();
                }
            }
        }
    }
}
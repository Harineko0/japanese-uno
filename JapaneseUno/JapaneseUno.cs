using System;

namespace JapaneseUno
{
    class JapaneseUno
    {
        static void Main(string[] args)
        {
            TableController controller = new TableController();
            TableConverter converter = new TableConverter();
            FileExporter exporter = new FileExporter();
            
            var tables = controller.Start(new GameConfig
            {
                maxCard = 2,
                playerNumber = 2,
            });

            var csvTables = converter.ToCSV(tables);

            string path = "C:/Users/hatin/RiderProjects/japanese-uno/";
            exporter.WriteDictionary(path + "export.csv", csvTables);
            
            foreach (var table in tables)
            {
                foreach (var history in table.History)
                {
                    Console.WriteLine("------------------------------\n" + history);
                }
                Console.WriteLine("------------------------------\n" + table);
                Console.WriteLine("==============================\n");
            }
        }
    }
}
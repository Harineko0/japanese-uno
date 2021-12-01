using System;

namespace JapaneseUno
{
    class JapaneseUno
    {

		// 52米からスタートしたとき、残り8枚になったタイミングでどちらがかつか、掛け金を2倍にする価値はあるか
        
        static void Main(string[] args)
        {
            TableController controller = new TableController();
            TableConverter converter = new TableConverter();
            FileExporter exporter = new FileExporter();
            
            var tables = controller.Start(new GameConfig
            {
                playerNumber = 2,
                maxCard = 2,
            });

            var csvTables = converter.ToCSV(tables);

            string path = "C:/Users/hatin/RiderProjects/japanese-uno/exports/";
            exporter.WriteDictionary(path + "3_2.csv", csvTables);
            
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
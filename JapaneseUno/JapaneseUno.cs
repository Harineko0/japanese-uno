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
                maxCard = 6,
                playerNumber = 2,
            });

            var csvTables = converter.ToCSV(tables);

            string path = "C:/Users/hatin/RiderProjects/japanese-uno/";
            exporter.WriteDictionary(path + "export.csv", csvTables);
        }
    }
}
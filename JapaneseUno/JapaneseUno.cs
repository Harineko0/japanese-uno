using System;

namespace JapaneseUno
{
    class JapaneseUno
    {
        // 52米からスタートしたとき、残り8枚になったタイミングでどちらがかつか、掛け金を2倍にする価値はあるか
        static void Main(string[] args)
        {
            // var controller = new TableController();
            // var converter = new TableConverter();
            // var exporter = new FileExporter();
            // var analyzer = new TableAnalyzer();
            var simulator = new SimpleSimulator();

            var config = new GameConfig
            {
                playerNumber = 3,
                maxCard = 7,
            };
            
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            
            // var tables = controller.Start(config);
            var results = simulator.StartGame(config);

            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Console.WriteLine($"　{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒");
            
            GameResult.Analyze(results);

            // analyzer.Analyze(tables);

            // Console.WriteLine("Converting table to csv...");
            // var csvTables = converter.ToCSV(tables, new CsvOptions
            // {
            //     HistoryLimit = -1,
            // });
            //
            // Console.WriteLine("Writing to file...");
            // string path = @"C:\Users\hatin\RiderProjects\japanese-uno\exports\";
            // exporter.WriteDictionary(path + config.playerNumber + "_" + config.maxCard + ".csv", csvTables, false);
        }
    }
}
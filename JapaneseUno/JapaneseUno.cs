﻿using System;

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

            var config = new GameConfig
            {
                playerNumber = 2,
                maxCard = 4,
            };
            
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            
            var tables = controller.Start(config);

            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Console.WriteLine($"　{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒");

            var csvTables = converter.ToCSV(tables);

            string path = @"C:\Users\hatin\RiderProjects\japanese-uno\exports\";
            exporter.WriteDictionary(path + config.playerNumber + "_" + config.maxCard + ".csv", csvTables);
            
            // foreach (var table in tables)
            // {
            //     foreach (var history in table.History)
            //     {
            //         Console.WriteLine("------------------------------\n" + history);
            //     }
            //     Console.WriteLine("------------------------------\n" + table);
            //     Console.WriteLine("==============================\n");
            // }
        }
    }
}
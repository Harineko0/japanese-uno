using System;
using NLog;

namespace JapaneseUno
{
    public class NLogService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Info(string str)
        {
            logger.Info(str);
        }

        public static void Debug(string str)
        {
            // logger.Debug(str);
            Console.WriteLine(str);
        }
    }
}
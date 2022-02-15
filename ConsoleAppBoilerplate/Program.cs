using System;
using NLog;

namespace ConsoleAppBoilerplate
{
    internal class Program
    {
        public static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            try
            {
                var consoleBase = new ConsoleBase();
                consoleBase.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Logger.Error(e);
                throw;
            }
        }
    }
}
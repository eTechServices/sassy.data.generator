using InvoiceBulkRegisteration.TrackApplication;
using sassy.bulk.BotHelper;
using System;

namespace InvoiceBulkRegisteration.Logging
{
    public class LogService: ILogService
    {
        public void Error(string message, string method, bool mailSend = default) => Console.WriteLine($"Error: {message} on Method: {method} {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        public void Information(string message) => Console.WriteLine($"Success: {message} {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        public void Warning(string message) => Console.WriteLine($"Warning: {message} {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        public void Exception(Exception exception)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{AssemblyInformation.Current} terminated unexpectedly.");
            Console.WriteLine(exception.ToString());
            Console.ForegroundColor = foregroundColor;
        }
        public void Welcome(string name)
        {
            Console.WriteLine("\t\tWelcome to Sassy POS Bulk Registeration App (Invoice)");
            Console.WriteLine($"\x0A   {name}{Bot.Welcome}");
        }
    }
}

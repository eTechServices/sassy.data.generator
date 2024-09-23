using InvoiceBulkRegisteration.Infrastructure;
using InvoiceBulkRegisteration.Logging;
using Microsoft.Extensions.Hosting;
using sassy.bulk.BuilderFactory;
using sassy.bulk.DataGenerator;
using System;
using System.Threading.Tasks;

namespace sassy.bulk
{
    internal class Program
    {
        private readonly ILogService _log;
        public Program(ILogService logService)
        {
            _log = logService;
        }
        static void Main(string[] args)
        {
            IHost host = null;
            host = ExeExtensions.AddSettings(args).Build();
            var program = ProgramFactory.InitilizeServiceFactory();
            program.RunProgram().ConfigureAwait(false);
            var data = Data.GenerateSampleData(10);
            foreach(var i in data)
            {
                Console.WriteLine(i.FirstName + i.LastName);
            }
            Console.ReadKey();
        }
        private async Task RunProgram()
        {
            _log.Welcome($"Ebad Hassan");
        }
    }
}

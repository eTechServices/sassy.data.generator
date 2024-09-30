using InvoiceBulkRegisteration.Infrastructure;
using InvoiceBulkRegisteration.Logging;
using Microsoft.Extensions.Hosting;
using sassy.bulk.BuilderFactory;
using System.Threading.Tasks;
using sassy.bulk.UIUtil;
using System.Reflection;
using System;
using sassy.bulk.DBContext;
using sassy.bulk.Cache;

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
            Console.Title = $"Sassy POS console app v{Assembly.GetExecutingAssembly().GetName().Version}";
            IHost host = null;
            host = ExeExtensions.AddSettings(args).Build();
            var program = ProgramFactory.InitilizeServiceFactory();
            program.RunProgram().GetAwaiter().GetResult();

            var restart = new RestartApplication();
            restart.StartScreen();

            Console.ReadKey();
        }
        private async Task RunProgram()
        {
           var login = new LoginScreen();
           login.StartScreen();

            if(Stack.Get(CacheKey.BearerToken).ToString() == "")
            {
                login.StartScreen();
            }
        }
    }
}

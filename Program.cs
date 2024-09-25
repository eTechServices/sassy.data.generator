using InvoiceBulkRegisteration.Infrastructure;
using InvoiceBulkRegisteration.Logging;
using Microsoft.Extensions.Hosting;
using sassy.bulk.BuilderFactory;
using System.Threading.Tasks;
using sassy.bulk.UIUtil;

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

            var restart = new RestartApplication();
            restart.StartScreen();
        }
        private async Task RunProgram()
        {
            var login = new LoginScreen();
            login.StartScreen();
        }
    }
}

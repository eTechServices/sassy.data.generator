using Microsoft.Extensions.Hosting;

namespace InvoiceBulkRegisteration.Infrastructure
{
    internal static class ExeExtensions
    {
        public static IHostBuilder AddSettings(string[] args) =>
            new HostBuilder()
            .ConfigureAppConfiguration(config => config.AddCustomConfiguration());
    }
}

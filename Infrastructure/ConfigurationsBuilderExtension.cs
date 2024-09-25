/**
 * @Author: Ebad Hassan
 * @Date:   2024-09-20 10:26:36
 * @Last Modified by:   Ebad Hassan
 * @Last Modified time: 2024-09-20 10:30:14
 */

using Microsoft.Extensions.Configuration;

namespace InvoiceBulkRegisteration.Infrastructure
{
    public static class ConfigurationsBuilderExtension
    {
        public static IConfigurationBuilder AddCustomConfiguration(this IConfigurationBuilder configurationBuilder) =>
            configurationBuilder
            .AddJsonFile($"app.settings.json", false, false);
    }
}

/**
 * @Author: Ebad Hassan
 * @Date:   2024-09-20 10:26:36
 * @Last Modified by:   Ebad Hassan
 * @Last Modified time: 2024-09-27 12:34:10
 */

using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace InvoiceBulkRegisteration.Infrastructure
{
    public static class ConfigurationsBuilderExtension
    {
        public static IConfigurationBuilder AddCustomConfiguration(this IConfigurationBuilder configurationBuilder) =>
            configurationBuilder
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.settings.json"), false, false);
    }
}

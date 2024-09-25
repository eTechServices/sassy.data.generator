using InvoiceBulkRegisteration.Logging;
using Microsoft.Extensions.DependencyInjection;
using sassy.bulk.UIUtil;

namespace sassy.bulk.BuilderFactory
{
    /// <summary>
    /// <para>This class is responsible for creating and configuring the application's dependencies using dependency injection.</para> 
    /// <para>It defines a static method `IntializeServiceFactory` that sets up and returns a configured `Program` objects as Services.</para>
    /// </summary>
    /// <remarks>
    /// <para>This class utilizes dependency injection to provide a flexible way to manage application dependencies.</para>
    /// </remarks>
    internal class ProgramFactory
    {
        /// <summary>
        /// This static method creates a new instance of `ServiceCollection` to register dependencies.
        /// It registers services as singletons and builds the service provider. Finally, it creates and configures a new `Program` object.
        /// </summary>
        /// <returns>A configured instance of the `Program` class.</returns>
        public static Program InitilizeServiceFactory()
        {
            var services = new ServiceCollection();
            services.AddTransient<ILogService, LogService>();

            var serviceProvider = services.BuildServiceProvider();
            return new Program(
                serviceProvider.GetService<ILogService>()
                );
        }
    }
}

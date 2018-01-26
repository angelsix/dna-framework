using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// Extension methods for the Dna Framework
    /// </summary>
    public static class FrameworkExtensions
    {
        /// <summary>
        /// Adds a default logger so that we can get a non-generic ILogger
        /// that will have the category name of "Dna"
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultLogger(this IServiceCollection services)
        {
            // Add a default logger
            services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("Dna"));

            // Return the services
            return services;
        }
    }
}

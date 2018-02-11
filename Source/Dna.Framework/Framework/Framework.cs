using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Dna
{
    /// <summary>
    /// The main entry point into the Dna Framework library
    /// </summary>
    public static class Framework
    {
        #region Private Members

        /// <summary>
        /// The dependency injection service provider
        /// </summary>
        private static IServiceProvider ServiceProvider;

        #endregion

        #region Public Properties

        /// <summary>
        /// The dependency injection service provider
        /// </summary>
        public static IServiceProvider Provider => ServiceProvider;

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public static IConfiguration Configuration => Provider.GetService<IConfiguration>();

        /// <summary>
        /// Gets the default logger
        /// </summary>
        public static ILogger Logger => Provider.GetService<ILogger>();

        /// <summary>
        /// Gets the framework environment
        /// </summary>
        public static FrameworkEnvironment Environment => Provider.GetService<FrameworkEnvironment>();

        /// <summary>
        /// Gets the framework exception handler
        /// </summary>
        public static IExceptionHandler ExceptionHandler => Provider.GetService<IExceptionHandler>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Should be called once a Framework Construction is finished and we want to build it and
        /// start our application
        /// </summary>
        /// <param name="construction">The construction</param>
        public static void Build(this FrameworkConstruction construction)
        {
            // Build the service provider
            ServiceProvider = construction.Services.BuildServiceProvider();

            // Log the startup complete
            Logger.LogCriticalSource($"Dna Framework started in {Environment.Configuration}...");
        }

        #endregion
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Dna
{
    /// <summary>
    /// The main entry point into the Dna Framework library
    /// </summary>
    /// <remarks>
    /// <para>
    ///     To use Dna.Framework you need to create a new <see cref="FrameworkConstruction"/>
    ///     such as <see cref="DefaultFrameworkConstruction"/> and then add your services
    ///     then finally <see cref="Framework.Build(FrameworkConstruction)"/>. For example:
    /// </para>
    /// <code>
    ///     // Create the default framework and build it
    ///     new DefaultFrameworkConstruction().Build();
    ///     
    ///     // Set Framework.Environment up based on this assemblies environment
    ///     Framework.SetEnvironment();
    /// </code>
    /// </remarks>
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
        /// Gets the logger factory for creating loggers
        /// </summary>
        public static ILoggerFactory LoggerFactory => Provider.GetService<ILoggerFactory>();

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
        public static FrameworkConstruction Build(this FrameworkConstruction construction)
        {
            // Build the service provider
            ServiceProvider = construction.Services.BuildServiceProvider();

            // Return construction
            return construction;
        }


        /// <summary>
        /// Sets up the <see cref="FrameworkEnvironment"/> variables such as
        /// <see cref="FrameworkEnvironment.IsDevelopment"/> based on the
        /// environment of the calling application
        /// </summary>
        [Conditional("DEBUG")]
        public static void SetEnvironment()
        {
            // Setup environment based on the fact this call has Conditional attribute the same
            // as the SetEnvironment call we are calling, so this call will run only if we are in the 
            // same Conditional attribute so long as these 2 calls match
            Environment.SetEnvironment();
        }

        /// <summary>
        /// Shortcut to Framework.Provider.GetService to get an injected service of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of service to get</typeparam>
        /// <returns></returns>
        public static T Service<T>()
        {
            // Use provider to get the service
            return Provider.GetService<T>();
        }

        #endregion
    }
}

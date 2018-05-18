using Microsoft.Extensions.DependencyInjection;
using System;
using static Dna.FrameworkDI;

namespace Dna
{
    /// <summary>
    /// The main entry point into the Dna Framework library
    /// </summary>
    /// <remarks>
    /// <para>
    ///     To use Dna.Framework you need to create a new <see cref="FrameworkConstruction"/>
    ///     such as <see cref="DefaultFrameworkConstruction"/> and then add your services
    ///     then finally <see cref="Framework.Build(FrameworkConstruction, bool)"/>. For example:
    /// </para>
    /// <code>
    /// 
    ///     // Create the default framework and build it
    ///     Framework.Construct&lt;DefaultFrameworkConstruction&gt;().Build();
    ///     
    /// </code>
    /// </remarks>
    public static class Framework
    {
        #region Public Properties

        /// <summary>
        /// The framework construction used in this application.
        /// NOTE: This should be set by the consuming application at the very start of the program
        /// </summary>
        /// <example>
        /// <code>
        ///     Framework.Construct&lt;DefaultFrameworkConstruction&gt;();
        /// </code>
        /// </example>
        public static FrameworkConstruction Construction { get; private set; }

        /// <summary>
        /// The dependency injection service provider
        /// </summary>
        public static IServiceProvider Provider => Construction.Provider;

        #endregion

        #region Extension Methods

        /// <summary>
        /// Should be called once a Framework Construction is finished and we want to build it and
        /// start our application
        /// </summary>
        /// <param name="construction">The construction</param>
        /// <param name="logStarted">Specifies if the Dna Framework Started message should be logged</param>
        public static void Build(this FrameworkConstruction construction, bool logStarted = true)
        {
            // Build the service provider
            construction.Build();

            // Log the startup complete
            if (logStarted)
                Logger.LogCriticalSource($"Dna Framework started in {FrameworkEnvironment.Configuration}...");
        }

        /// <summary>
        /// Should be called once a Framework Construction is finished and we want to build it and
        /// start our application in a hosted environment where the service provider is already built
        /// such as ASP.Net Core applications
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <param name="logStarted">Specifies if the Dna Framework Started message should be logged</param>
        public static void Build(IServiceProvider provider, bool logStarted = true)
        {
            // Build the service provider
            Construction.Build(provider);

            // Log the startup complete
            if (logStarted)
                Logger.LogCriticalSource($"Dna Framework started in {FrameworkEnvironment.Configuration}...");
        }

        /// <summary>
        /// The initial call to setting up and using the Dna Framework
        /// </summary>
        /// <typeparam name="T">The type of construction to use</typeparam>
        public static FrameworkConstruction Construct<T>()
            where T : FrameworkConstruction, new()
        {
            Construction = new T();

            // Return construction for chaining
            return Construction;
        }


        /// <summary>
        /// The initial call to setting up and using the Dna Framework.
        /// </summary>
        /// <typeparam name="T">The type of construction to use</typeparam>
        /// <param name="constructionInstance">The instance of the construction to use</param>
        public static FrameworkConstruction Construct<T>(T constructionInstance)
            where T : FrameworkConstruction
        {
            // Set construction
            Construction = constructionInstance;

            // Return construction for chaining
            return Construction;
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

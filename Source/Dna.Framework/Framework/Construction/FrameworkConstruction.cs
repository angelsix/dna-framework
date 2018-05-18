using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dna
{
    /// <summary>
    /// The construction information when starting up and configuring Dna.Framework
    /// </summary>
    public class FrameworkConstruction
    {
        #region Protected Members

        /// <summary>
        /// The services that will get used and compiled once the framework is built
        /// </summary>
        protected IServiceCollection mServices;

        #endregion

        #region Public Properties

        /// <summary>
        /// The dependency injection service provider
        /// </summary>
        public IServiceProvider Provider { get; protected set; }

        /// <summary>
        /// The services that will get used and compiled once the framework is built
        /// </summary>
        public IServiceCollection Services
        {
            get => mServices;
            set
            {
                // Set services
                mServices = value;

                // If we have some...
                if (mServices != null)
                    // Inject environment into services
                    Services.AddSingleton(Environment);
            }
        }

        /// <summary>
        /// The environment used for the Dna.Framework
        /// </summary>
        public IFrameworkEnvironment Environment { get; protected set; }

        /// <summary>
        /// The configuration used for the Dna.Framework
        /// </summary>
         public IConfiguration Configuration { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="createServiceCollection">If true, a new <see cref="ServiceCollection"/> will be created for the Services</param>
        public FrameworkConstruction(bool createServiceCollection = true)
        {
            // Create environment details
            Environment = new DefaultFrameworkEnvironment();

            // If we should create the service collection
            if (createServiceCollection)
                // Create a new list of dependencies
                Services = new ServiceCollection();
        }

        #endregion

        #region Build Methods

        /// <summary>
        /// Builds the service collection into a service provider
        /// </summary>
        public void Build(IServiceProvider provider = null)
        {
            // Use given provider or build it
            Provider = provider ?? Services.BuildServiceProvider();
        }

        #endregion

        #region Hosted Environment Methods

        /// <summary>
        /// Uses the given service collection in the framework. 
        /// Typically used in an ASP.Net Core environment where
        /// the ASP.Net server has its own collection.
        /// </summary>
        /// <param name="services">The services to use</param>
        /// <returns></returns>
        public FrameworkConstruction UseHostedServices(IServiceCollection services)
        {
            // Set services
            Services = services;

            // Return self for chaining
            return this;
        }

        /// <summary>
        /// Uses the given configuration in the framework
        /// </summary>
        /// <param name="configuration">The configuration to use</param>
        /// <returns></returns>
        public FrameworkConstruction UseConfiguration(IConfiguration configuration)
        {
            // Set configuration
            Configuration = configuration;

            // Return self for chaining
            return this;
        }

        #endregion
    }
}

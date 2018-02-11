using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dna
{
    /// <summary>
    /// The construction information when starting up and configuring Dna.Framework
    /// </summary>
    public class FrameworkConstruction
    {
        #region Public Properties

        /// <summary>
        /// The services that will get used and compiled once the framework is built
        /// </summary>
        public IServiceCollection Services { get; set; }

        /// <summary>
        /// The environment used for the Dna.Framework
        /// </summary>
        public FrameworkEnvironment Environment { get; set; }

        /// <summary>
        /// The configuration used for the Dna.Framework
        /// </summary>
         public IConfiguration Configuration { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FrameworkConstruction()
        {
            // Create a new list of dependencies
            Services = new ServiceCollection();

            // Create environment details
            Environment = new FrameworkEnvironment();

            // Inject environment into services
            Services.AddSingleton(Environment);
        }

        #endregion
    }
}

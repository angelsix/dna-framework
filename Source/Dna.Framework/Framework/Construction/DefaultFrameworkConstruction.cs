using Microsoft.Extensions.Configuration;
using System;

namespace Dna
{
    /// <summary>
    /// Creates a default framework construction containing all 
    /// the default configuration and services
    /// </summary>
    public class DefaultFrameworkConstruction : FrameworkConstruction
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultFrameworkConstruction(Action<IConfigurationBuilder> configure = null)
        {
            // Configure...
            this.Configure(configure)
                // And add default services
                .AddDefaultServices();
        }

        #endregion
    }
}

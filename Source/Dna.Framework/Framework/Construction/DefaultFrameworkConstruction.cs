using Microsoft.Extensions.Configuration;
using System;

namespace Dna
{
    /// <summary>
    /// Creates a default framework construction containing all 
    /// the default configuration and services
    /// </summary>
    /// <example>
    /// 
    /// <para>
    ///     This is the expected setup code for building a Dna Framework Construction
    /// </para>
    /// 
    /// <code>
    ///     // Build the framework adding any required services
    ///     var framework = new DefaultFrameworkConstruction()
    ///             .AddFileLogger()
    ///             .AddAutoUploader()
    ///             .Build();
    ///             
    ///     // Configure services
    ///     // Sets up environment variables based on the calling assembly
    ///     Framework.SetEnvironment();
    ///     
    ///     // Custom service extensions
    ///     framework.UseYourService1(options => options.Something = true );
    ///     framework.UseYourService2();
    /// </code>
    /// 
    /// </example>
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

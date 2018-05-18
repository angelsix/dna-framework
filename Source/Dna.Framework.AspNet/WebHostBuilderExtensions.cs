using Microsoft.AspNetCore.Hosting;
using System;

namespace Dna.AspNet
{
    /// <summary>
    /// Extensions for <see cref="IWebHostBuilder"/>
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Adds the Dna Framework construct to the ASP.Net Core application
        /// </summary>
        /// <param name="builder">The web host builder</param>
        /// <param name="configure">Custom action to configure the Dna Framework</param>
        /// <returns></returns>
        public static IWebHostBuilder UseDnaFramework(this IWebHostBuilder builder, Action<FrameworkConstruction> configure = null)
        {
            builder.ConfigureServices((context, services) =>
            {
                // Construct a hosted Dna Framework
                Framework.Construct<HostedFrameworkConstruction>();

                // Setup this service collection to
                // be used by DnaFramework 
                services.AddDnaFramework()
                        // Add configuration
                        .AddConfiguration(context.Configuration);

                // Fire off construction configuration
                configure?.Invoke(Framework.Construction);

                // NOTE: Framework will do .Build() from the Startup.cs Configure call
                //       app.UseDnaFramework()
            });

            // Return builder for chaining
            return builder;
        }
    }
}

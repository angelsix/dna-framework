namespace Dna
{
    /// <summary>
    /// Creates a default framework construction containing all 
    /// the default configuration and services, when used inside
    /// a project that has it's own service provider such as an
    /// ASP.Net Core website
    /// </summary>
    /// <example>
    /// 
    /// <para>
    ///     This is an example setup code for building a Dna Framework Construction
    ///     if you include the Dna.Framework.AspNet NuGet package
    /// </para>
    /// 
    /// <code>
    /// 
    ///     //  Program.cs (in BuildWebHost)
    ///     // ------------------------------
    ///             
    ///         return WebHost.CreateDefaultBuilder()
    ///             // Merge Dna Framework into ASP.Net Core environment
    ///             .UseDnaFramework(construct =>
    ///             {
    ///                 // Add file logger
    ///                 construct.AddFileLogger();
    ///      
    ///                 //
    ///                 // NOTE: If you want to configure anything in ConfigurationBuilder just use 
    ///                 //       ConfigureAppConfiguration(builder => {}) and then you  have
    ///                 //       access to Dna.Framework.Environment and Construction at that point
    ///                 //       like the normal flow of Dna Framework setup
    ///                 //
    ///      
    ///                 // The last step is inside Startup Configure method to call 
    ///             })
    ///             .UseStartup&lt;Startup&gt;()
    ///             .Build();
    ///
    ///     //  Startup.cs (in Configure)
    ///     // ---------------------------
    ///     
    ///         // Use Dna Framework
    ///         app.UseDnaFramework();
    /// 
    /// </code>
    /// 
    /// </example>
    public class HostedFrameworkConstruction : FrameworkConstruction
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HostedFrameworkConstruction() : base(createServiceCollection: false)
        {

        }

        #endregion
    }
}

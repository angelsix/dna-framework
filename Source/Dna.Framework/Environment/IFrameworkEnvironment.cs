namespace Dna
{
    /// <summary>
    /// Details about the current framework environment
    /// </summary>
    public interface IFrameworkEnvironment
    {
        /// <summary>
        /// The configuration of the environment, typically Development or Production
        /// </summary>
        string Configuration { get; }

        /// <summary>
        /// True if we are in a development (specifically, debuggable) environment
        /// </summary>
        bool IsDevelopment { get; }

        /// <summary>
        /// Indicates if we are a mobile platform
        /// </summary>
        bool IsMobile { get; }
    }
}
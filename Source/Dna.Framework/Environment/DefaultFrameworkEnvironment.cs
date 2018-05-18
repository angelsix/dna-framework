using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Dna
{
    /// <summary>
    /// Default implementation about the current framework environment
    /// </summary>
    public class DefaultFrameworkEnvironment : IFrameworkEnvironment
    {
        #region Public Properties

        /// <summary>
        /// True if we are in a development (specifically, debuggable) environment
        /// </summary>
        public bool IsDevelopment => Assembly.GetEntryAssembly()?.GetCustomAttribute<DebuggableAttribute>()?.IsJITTrackingEnabled == true;

        /// <summary>
        /// The configuration of the environment, either Development or Production
        /// </summary>
        public string Configuration => IsDevelopment ? "Development" : "Production";

        /// <summary>
        /// Determines (crudely) if we are a mobile (Xamarin) platform.
        /// This is a temporary, fragile check until it is officially supported 
        /// https://github.com/dotnet/corefx/issues/27417
        /// </summary>
        public bool IsMobile => RuntimeInformation.FrameworkDescription?.ToLower().Contains("mono") == true;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultFrameworkEnvironment()
        {

        }

        #endregion
    }
}

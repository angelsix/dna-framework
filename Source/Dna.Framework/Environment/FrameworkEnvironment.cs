using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Dna
{
    /// <summary>
    /// Details about the current system environment
    /// </summary>
    public class FrameworkEnvironment
    {
        #region Public Properties

        /// <summary>
        /// A flag indicating if the environment is in debug. 
        /// This should be set manually or by calling Framework.SetEnvironment() 
        /// from the calling application.
        /// </summary>
        public bool IsDevelopment { get; set; }

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
        public FrameworkEnvironment()
        {
            // So if we are inside this framework or referencing the source code directly
            // then this #if will compile, run and set the mIsDevelopment correctly
            // without _requiring_ calling Framework.SetEnvironment()
            //
            // If we instead package this to a NuGet and so compile in Release mode
            // this line will be removed and it will fall back to requiring either 
            // manually setting IsDevelopment or calling Framework.SetEnvironment()
#if DEBUG
            SetEnvironment();
#endif
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets up the environment variables such as <see cref="IsDevelopment"/>
        /// based on the environment of the calling application
        /// </summary>
        [Conditional("DEBUG")]
        public void SetEnvironment()
        {
            // Set the IsDevelopment based on if the calling application 
            // has the DEBUG symbol constant
            IsDevelopment = true;
        }

        #endregion
    }
}

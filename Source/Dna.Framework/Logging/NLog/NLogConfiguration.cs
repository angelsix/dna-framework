using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// The configuration for the NLog logger
    /// </summary>
    public class NLogConfiguration
    {
         #region Public Properties

        /// <summary>
        /// The level of log that should be processed
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;

        /// <summary>
        /// Whether to log the time as part of the message
        /// </summary>
        public bool LogTime { get; set; } = true;

        /// <summary>
        /// Indicates if the log level should be output as part of the log message
        /// </summary>
        public bool OutputLogLevel { get; set; } = true;

        #endregion
    }
}
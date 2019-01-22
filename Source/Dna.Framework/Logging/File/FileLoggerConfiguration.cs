using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// The configuration for a <see cref="FileLogger"/>
    /// </summary>
    public class FileLoggerConfiguration
    {
        #region Public Properties

        /// <summary>
        /// The level of log that should be processed
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;

        /// <summary>
        /// Whether the log should include the level for each message
        /// (and then you can easily scan logs for, say ,ERR for errors
        /// </summary>
        public bool IncludeLoglevel { get; set; } = false;

        /// <summary>
        /// Whether the log should include the thread ID which the message comes from
        /// </summary>
        public bool IncludeThreadId { get; set; } = false;

        /// <summary>
        /// Whether to log the time as part of the message
        /// </summary>
        public bool LogTime { get; set; } = true;

        /// <summary>
        /// The format string used for logging the time (if LogTime is true)
        /// </summary>
        public string TimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Configuration options for rotating log files.
        /// These are required to prevent consuming unlimited storage space.
        /// Default: rotation disabled.
        /// </summary>
        public LogRotationConfiguration RotationConfig { get; set; } = new LogRotationConfiguration();

        #endregion
    }
}

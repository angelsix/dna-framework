using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// A logger that writes the logs to file
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Static Properties

        /// <summary>
        /// A list of file locks based on path
        /// </summary>
        protected static ConcurrentDictionary<string, object> FileLocks = new ConcurrentDictionary<string, object>();

        #endregion

        #region Protected Members

        /// <summary>
        /// The category for this logger
        /// </summary>
        protected readonly string mCategoryName;

        /// <summary>
        /// The file path to write to
        /// </summary>
        protected readonly string mFilePath;

        /// <summary>
        /// The directory the file is in
        /// </summary>
        protected readonly string mDirectory;

        /// <summary>
        /// The configuration to use
        /// </summary>
        protected FileLoggerConfiguration mConfiguration;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="categoryName">The category for this logger</param>
        /// <param name="filePath">The file path to write to</param>
        /// <param name="configuration">The configuration to use</param>
        public FileLogger(string categoryName, string filePath, FileLoggerConfiguration configuration)
        {
            // Get absolute path
            filePath = Path.GetFullPath(filePath);

            // Set members
            mCategoryName = categoryName;
            mFilePath = filePath;
            mDirectory = Path.GetDirectoryName(filePath);
            mConfiguration = configuration;
        }

        #endregion

        /// <summary>
        /// File loggers are not scoped so this is always null
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Enabled if the log level is the same or greater than the configuration
        /// </summary>
        /// <param name="logLevel">The log level to check against</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            // Enabled if the log level is greater or equal to what we want to log
            return logLevel >= mConfiguration.LogLevel;
        }

        /// <summary>
        /// Logs the message to file
        /// </summary>
        /// <typeparam name="TState">The type of the details of the message</typeparam>
        /// <param name="logLevel">The log level</param>
        /// <param name="eventId">The Id</param>
        /// <param name="state">The details of the message</param>
        /// <param name="exception">Any exception to log</param>
        /// <param name="formatter">The formatter for converting the state and exception to a message string</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // If we should not log...
            if (!IsEnabled(logLevel))
                // Return
                return;

            // Get current time
            var currentTime = DateTimeOffset.Now.ToString(mConfiguration.TimeFormat);

            // Prepend the time to the log if desired
            var timeLogString = mConfiguration.LogTime ? $"[{currentTime}] " : "";

            // Get the formatted message string
            var message = formatter(state, exception);

            var messageBuilder = new StringBuilder();

            // Build the message
            messageBuilder.Append(timeLogString);
            AppendCurrentThreadId(messageBuilder);
            AppendLogLevelMnemonic(messageBuilder, logLevel);
            messageBuilder.AppendLine(message);

            if (exception != null)
            {
                // exception message
                messageBuilder.AppendLine(exception.ToString());
            }

            // Normalize path
            // TODO: Make use of configuration base path
            var normalizedPath = mFilePath.ToUpper();

            // Get the file lock based on the absolute path
            var fileLock = FileLocks.GetOrAdd(normalizedPath, path => new object());

            // Lock the file
            lock (fileLock)
            {
                EnsureTargetDirectoryExists();

                // Write the message to the file
                File.AppendAllText(mFilePath, messageBuilder.ToString());
            }
        }

        /// <summary>
        /// appends the string representation of the specified logLevel
        /// to the specified messageBuilder
        /// </summary>
        /// <param name="messageBuilder"></param>
        /// <param name="logLevel"></param>
        private void AppendLogLevelMnemonic(StringBuilder messageBuilder, LogLevel logLevel)
        {
            if (mConfiguration.IncludeLoglevel)
            {
                LogLevelConverter.Append(messageBuilder, logLevel);
            }
        }

        private void AppendCurrentThreadId(StringBuilder messageBuilder)
        {
            if (mConfiguration.IncludeThreadId)
            {
                messageBuilder.Append(System.Threading.Thread.CurrentThread.ManagedThreadId);
            }
        }

        private void EnsureTargetDirectoryExists()
        {
            if (!Directory.Exists(mDirectory))
                Directory.CreateDirectory(mDirectory);
        }
    }
}

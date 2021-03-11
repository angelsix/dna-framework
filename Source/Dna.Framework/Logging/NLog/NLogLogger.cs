using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Dna
{
    public class NLogLogger : ILogger
    {
        protected NLogConfiguration mConfiguration;

        private NLog.Logger mLogger;

        public NLogLogger(string categoryName, string filePath, NLogConfiguration configuration)
        {
            mConfiguration = configuration;

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = Path.GetFullPath(filePath) };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logfile);
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);

            // Apply config           
            NLog.LogManager.Configuration = config;

            mLogger = NLog.LogManager.GetLogger(categoryName);
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return logLevel >= mConfiguration.LogLevel;
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // if we should log
            if (!IsEnabled(logLevel))
            {
                // Return
                return;
            }

            // Get current time
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // Prepend log level
            var logLevelString = mConfiguration.OutputLogLevel ? $"[{logLevel.ToString().ToUpper()}] " : "";

            // prepend log level
            var timeLogString = mConfiguration.LogTime ? $"[{currentTime}] " : "";

            // Get the formatted message string
            var message = formatter(state, exception);

            // Write the message
            var output = $"{logLevelString}{timeLogString}{message}";

            mLogger.Log(ConvertLogLevel(logLevel), output);
        }


        /// <summary>
        /// Convert log level to NLog variant.
        /// </summary>
        /// <param name="logLevel">level to be converted.</param>
        /// <returns></returns>
        private static NLog.LogLevel ConvertLogLevel(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return NLog.LogLevel.Info;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return NLog.LogLevel.Warn;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return NLog.LogLevel.Error;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return NLog.LogLevel.Fatal;
                case Microsoft.Extensions.Logging.LogLevel.None:
                    return NLog.LogLevel.Off;
                default:
                    return NLog.LogLevel.Debug;
            }
        }
    }
}
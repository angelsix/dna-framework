using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Dna
{
    public static class LogLevelConverter
    {
        /// <summary>
        /// the log levels dictioinary
        /// </summary>
        private static readonly Dictionary<LogLevel, string> Levels;

        static LogLevelConverter()
        {
            Levels = new Dictionary<LogLevel, string>()
            {
                { LogLevel.Trace,       "TRACE " },
                { LogLevel.Debug,       "DEBUG " },
                { LogLevel.Information, "INFO  " },
                { LogLevel.Warning,     "WARN  " },
                { LogLevel.Error,       "ERROR " },
                { LogLevel.Critical,    "CRIT  " },
                { LogLevel.None,        "NONE  " },
            };
        }

        /// <summary>
        /// Converts a logLevel to constant-length string representation.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns>
        /// Returns mnemonic value corresponding to the value of logLevel.
        /// Returns 'UNKNO ' for an unsupported value.
        /// </returns>
        public static string Convert(LogLevel logLevel)
        {
            if (Levels.TryGetValue(logLevel, out string value))
            {
                return value;
            }
            return "UNKNO ";
        }
    }
}

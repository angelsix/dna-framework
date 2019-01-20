using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;

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

        public static void Append(StringBuilder messageBuilder, LogLevel logLevel)
        {
            messageBuilder.Append(Levels[logLevel]);
        }

    }
}

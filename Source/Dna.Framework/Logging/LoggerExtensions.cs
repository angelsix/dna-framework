using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace Dna
{
    /// <summary>
    /// Extensions for <see cref="ILogger"/> loggers
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Logs a critical message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        /// <param name="args">The additional arguments</param>
        public static void LogCriticalSource(
            this ILogger logger, 
            string message, 
            EventId eventId = new EventId(),
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args) => logger.Log(LogLevel.Critical, eventId, args.Prepend(origin, filePath, lineNumber, message), exception, LoggerSourceFormatter.Format);
    }
}

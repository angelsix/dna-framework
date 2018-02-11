using System;

namespace Dna
{
    /// <summary>
    /// Handles all exceptions, simply logging them to the logger
    /// </summary>
    public class BaseExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// Logs the given exception
        /// </summary>
        /// <param name="exception">The exception</param>
        public void HandleError(Exception exception)
        {
            // Log it
            // TODO: Localization of strings
            Framework.Logger.LogCriticalSource("Unhandled exception occurred", exception: exception);
        }
    }
}

namespace Dna
{
    using System;
    using System.IO;

    /// <summary>
    /// writes log mesages to the specified file without any rotation policy
    /// the file is opened/closed after each write
    /// </summary>
    internal sealed class SimpleFileWriter : IFileLogWriter
    {
        private readonly string _logFilePath;

        /// <summary>
        /// Creates a simple log writer
        /// </summary>
        /// <param name="logFilePath"></param>
        public SimpleFileWriter(string logFilePath)
        {
            _logFilePath = logFilePath ?? throw new ArgumentNullException(nameof(logFilePath));

            var directory = Path.GetDirectoryName(logFilePath);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public void WriteLogMessage(string message)
        {
            File.WriteAllText(_logFilePath, message);
        }
    }
}

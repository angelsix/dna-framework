namespace Dna
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class RotatingFileWriter
    {
        private readonly LogRotationConfiguration _rotationConfig;
        private readonly string _logFilePath;
        private StreamWriter _logStream;

        /// <summary>
        /// Creates a rotating log writer with the specified configuration
        /// </summary>
        /// <param name="logFilePath"></param>
        /// <param name="config"></param>
        public RotatingFileWriter(string logFilePath, LogRotationConfiguration config)
        {
            _logFilePath = logFilePath ?? throw new ArgumentNullException(nameof(logFilePath));
            _rotationConfig = config ?? throw new ArgumentNullException(nameof(config));

            var directory = Path.GetDirectoryName(logFilePath);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            _logStream = OpenFileStream(_logFilePath);
        }


        public void WriteLogMessage(string message)
        {
            _logStream.Write(message);
            RotateLogAsNeeded();
        }

        private void RotateLogAsNeeded()
        {
            if (_rotationConfig.MaxLogFileSize == LogRotationConfiguration.Unlimited)
            {
                return;
            }
            var fullFileName = _logFilePath;
            var size = new FileInfo(_logFilePath).Length;
            if (size > _rotationConfig.MaxLogFileSize)
            {
                // time to rotate
                var fileName = Path.GetFileNameWithoutExtension(fullFileName);
                var extension = Path.GetExtension(fullFileName);
                var fullPath = Path.GetDirectoryName(fullFileName);

                var dateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var rotatedFileName = fileName + "_" + dateTime + extension;

                var rotatedName = Path.Combine(fullPath, rotatedFileName);

                // close the file before renaming it
                _logStream.Dispose();
                try
                {
                    File.Move(fullFileName, rotatedName);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Unable to rotate log file {fullFileName}: {ex.Message}");
                }

                _logStream = OpenFileStream(_logFilePath);

                // leave at most mConfiguration.RotationConfig.MaxLogFilesCount files
                RemoveExtraLogFiles(fullPath, fileName);
            }
        }

        private static StreamWriter OpenFileStream(string normalizedPath)
        {
            var fileExists = File.Exists(normalizedPath);
            return new StreamWriter(normalizedPath, fileExists, Encoding.UTF8, 64 * 1024);
        }

        /// <summary>
        /// removes the extra log files, assuming that the 'current' log file doesn't exist
        /// (as it has just been renamed to a new 'rotated' name)
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="baseFileNameNoExtension"></param>
        private void RemoveExtraLogFiles(string fullPath, string baseFileNameNoExtension)
        {
            if (_rotationConfig.MaxLogFilesCount == LogRotationConfiguration.Unlimited) return;

            // get all files in the log directory matching the pattern
            var files = Directory.GetFiles(fullPath, baseFileNameNoExtension + "_*");
            var toRemove = files.Length - _rotationConfig.MaxLogFilesCount;
            if (toRemove < 1) return;

            // sort the files from the oldest (stamped with earlier date & time) to the newest
            var sortedFiles = files.OrderBy(s => s).ToArray();
            for (int i = 0; i < toRemove; ++i)
            {
                var file = sortedFiles[i];
                try
                {
                    File.Delete(sortedFiles[i]);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error removing extra log file {file}: {ex.Message}");
                }
            }
        }
    }
}

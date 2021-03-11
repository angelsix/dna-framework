using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Dna
{
    public class NLogProvider : ILoggerProvider
    {
        protected string mFilePath;

        protected readonly NLogConfiguration mConfiguration;

        protected readonly ConcurrentDictionary<string, NLogLogger> mLoggers = new ConcurrentDictionary<string, NLogLogger>();
        
        public NLogProvider(string path, NLogConfiguration configuration)
        {
            // Set the configuration
            mConfiguration = configuration;

            // Set the path
            mFilePath = path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return mLoggers.GetOrAdd(categoryName, name => new NLogLogger(name, mFilePath, mConfiguration));
        }

        public void Dispose()
        {
            mLoggers.Clear();
        }
    }
}
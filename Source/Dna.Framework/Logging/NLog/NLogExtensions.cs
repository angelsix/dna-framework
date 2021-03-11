using Microsoft.Extensions.Logging;

namespace Dna
{
    public static class NLogExtensions
    {
        public static ILoggingBuilder AddNLog(this ILoggingBuilder builder, string path, NLogConfiguration configuration = null)
        {
            if (configuration == null)
            {
                configuration = new NLogConfiguration();
            }

            builder.AddProvider(new NLogProvider(path, configuration));

            return builder;
        }
    }
}
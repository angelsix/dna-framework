namespace Dna
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LogRotationConfiguration
    {
        public const int Unlimited = -1;

        public int MaxLogFileSize { get; set; } = Unlimited;

        public int MaxLogFilesCount { get; set; } = Unlimited;
    }
}

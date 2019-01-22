namespace Dna
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LogRotationConfiguration
    {
        public const int Unlimited = -1;

        /// <summary>
        /// The maximum allowed size of the log file (in bytes)
        /// </summary>
        public int MaxLogFileSize { get; set; } = Unlimited;

        /// <summary>
        /// The maximum number of rotated files to preserve
        /// (if 0, only the 'regular' log file will be preserved)
        /// </summary>
        public int MaxLogFilesCount
        {
            get
            {
                return mMaxLogFilesCount;
            }
            set
            {
                if (value < Unlimited)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The value must be greater than or equal to 0 or Unlimited");
                }
                mMaxLogFilesCount = value;
            }
        }

        private int mMaxLogFilesCount = Unlimited;
    }
}

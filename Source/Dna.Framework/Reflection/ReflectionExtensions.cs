using System;
using System.IO;

namespace Dna
{
    /// <summary>
    /// Extension methods for reflection methods
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the physical file location of the assembly where this type is stored
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>Returns the file location of the assembly in which this type is stored</returns>
        public static string FileLocation(this Type type)
        {
            return type.Assembly.Location;
        }

        /// <summary>
        /// Gets the physical folder location of the assembly where this type is stored
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>Returns the folder location of the assembly file in which this type is stored</returns>
        public static string FolderLocation(this Type type)
        {
            return Path.GetDirectoryName(type.Assembly.Location);
        }
    }
}

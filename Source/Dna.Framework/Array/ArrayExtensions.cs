using System.Collections.Generic;

namespace Dna
{
    /// <summary>
    /// Extension methods for arrays
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Append the given objects to the original source array
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="source">The original array of values</param>
        /// <param name="toAdd">The values to append to the source</param>
        /// <returns></returns>
        public static T[] Append<T>(this T[] source, params T[] toAdd)
        {
            // Create a list of the original items
            var list = new List<T>(source);

            // Append the new items
            list.AddRange(toAdd);

            // Return the new array
            return list.ToArray();
        }

        /// <summary>
        /// Prepend the given objects to the original source array
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="source">The original array of values</param>
        /// <param name="toAdd">The values to prepend to the source</param>
        /// <returns></returns>
        public static T[] Prepend<T>(this T[] source, params T[] toAdd)
        {
            // Create a list of the new items
            var list = new List<T>(toAdd);

            // Append the source items
            list.AddRange(source);

            // Return the new array
            return list.ToArray();
        }
    }
}

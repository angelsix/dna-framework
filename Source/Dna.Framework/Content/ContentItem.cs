namespace Dna
{
    /// <summary>
    /// Details about an item that can be used to pass around the Dna Framework providing basic trackable details
    /// on a file that exists on a system
    /// </summary>
    public class ContentItem
    {
        /// <summary>
        /// The uniquely identifiable key that identifies this item from all others 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <para>
        ///     The local path to the file useful for general viewing or details about where the file is.
        /// </para>
        /// <para>
        ///     This may be a virtual path on systems like iOS and Android, and full file paths on desktop systems.
        /// </para>
        /// </summary>
        public string LocalPath { get; set; }
    }
}

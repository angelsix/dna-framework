namespace Dna
{
    /// <summary>
    /// Known types of content that can be serialized and sent to a receiver
    /// </summary>
    public enum KnownContentSerializers
    {
        /// <summary>
        /// The data should be serialized to JSON
        /// </summary>
        Json = 1,

        /// <summary>
        /// The data should be serialized to XML
        /// </summary>
        Xml
    }
}

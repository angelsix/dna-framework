namespace Dna
{
    /// <summary>
    /// Extension methods for <see cref="KnownContentSerializers"/>
    /// </summary>
    public static class KnownContentSerializersExtensions
    {
        /// <summary>
        /// Takes a known serializer type and returns the Mime type associated with it
        /// </summary>
        /// <param name="serializer">The serializer</param>
        /// <returns></returns>
        public static string ToMimeString(this KnownContentSerializers serializer)
        {
            // Switch on the serializer
            switch (serializer)
            {
                // Json
                case KnownContentSerializers.Json:
                    return "application/json";

                // XML
                case KnownContentSerializers.Xml:
                    return "application/xml";

                // Unknown
                default:
                    return "application/octet-stream";
            }
        }
    }
}

using System.IO;
using System.Net;

namespace Dna
{
    /// <summary>
    /// Extension methods for <see cref="HttpWebResponse"/>
    /// </summary>
    public static class HttpWebResponseExtensions
    {
        /// <summary>
        /// Returns a <see cref="WebRequestResult{T}"/> pre-populated with the <see cref="HttpWebResponse"/> information
        /// </summary>
        /// <typeparam name="TResponse">The type of response to create</typeparam>
        /// <param name="serverResponse">The server response</param>
        /// <returns></returns>
        public static WebRequestResult<TResponse> CreateWebRequestResult<TResponse>(this HttpWebResponse serverResponse)
        {
            // Return a new web request result
            var result = new WebRequestResult<TResponse>
            {
                // Content type
                ContentType = serverResponse.ContentType,

                // Headers
                Headers = serverResponse.Headers,

                // Cookies
                Cookies = serverResponse.Cookies,

                // Status code
                StatusCode = serverResponse.StatusCode,

                // Status description
                StatusDescription = serverResponse.StatusDescription,
            };

            // If we got a successful response...
            if (result.StatusCode == HttpStatusCode.OK)
            {
                // Open the response stream...
                using (var responseStream = serverResponse.GetResponseStream())
                // Get a stream reader...
                using (var streamReader = new StreamReader(responseStream))
                    // Read in the response body
                    // NOTE: By reading to the end of the stream
                    //       The stream will also close for us
                    //       (which we must do to release the request)
                    result.RawServerResponse = streamReader.ReadToEnd();
            }

            return result;
        }
    }
}

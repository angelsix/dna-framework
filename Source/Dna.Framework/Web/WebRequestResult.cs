using System.Net;

namespace Dna
{
    /// <summary>
    /// A web response from a call to get generic object data from a HTTP server
    /// </summary>
    public class WebRequestResult
    {
        /// <summary>
        /// If the call was successful
        /// </summary>
        public bool Successful => ErrorMessage == null;

        /// <summary>
        /// If something failed, this is the error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The status description
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// The type of content returned by the server
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// All the response headers
        /// </summary>
        public WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// Any cookies sent in the response
        /// </summary>
        public CookieCollection Cookies { get; set; }

        /// <summary>
        /// The raw server response body
        /// </summary>
        public string RawServerResponse { get; set; }

        /// <summary>
        /// The actual server response as an object
        /// </summary>
        public object ServerResponse { get; set; }
    }

    /// <summary>
    /// A web response from a call to get specific data from a HTTP server
    /// </summary>
    /// <typeparam name="T">The type of data to deserialize the raw body into</typeparam>
    public class WebRequestResult<T> : WebRequestResult
    {
        /// <summary>
        /// Casts the underlying object to the specified type
        /// </summary>
        public new T ServerResponse { get => (T)base.ServerResponse; set => base.ServerResponse = value; }
    }
}

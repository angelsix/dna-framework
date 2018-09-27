using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Dna
{
    /// <summary>
    /// Continually hits up a web HTTP/HTTPS endpoint checking for a valid response.
    /// Good use for checking the connectivity of a website constantly
    /// </summary>
    public class HttpEndpointChecker : IDisposable
    {
        #region Protected Members

        /// <summary>
        /// Flag indicating if this class is disposing
        /// </summary>
        protected bool mDisposing;

        /// <summary>
        /// The callback that is called when the connectivity state changes
        /// </summary>
        protected Action<bool> mStateChangedCallback;

        #endregion

        #region Public Properties

        /// <summary>
        /// The endpoint being checked
        /// </summary>
        public string Endpoint { get; protected set; }

        /// <summary>
        /// Indicates if the endpoint is responsive (at the last interval returned a valid response)
        /// </summary>
        public bool Responsive { get; set; }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="endpoint">The endpoint to do a GET call on</param>
        /// <param name="interval">The time between periodical checks, in milliseconds</param>
        /// <param name="stateChangedCallback">Fired when the state changes of the endpoint</param>
        /// <param name="validResponseParser">If specified, handles whether the given response from the endpoint is classed as successful or not</param>
        /// <param name="logger">The logger to use for logging messages</param>
        public HttpEndpointChecker(string endpoint, int interval, Action<bool> stateChangedCallback, Func<HttpWebResponse, Exception, bool> validResponseParser = null, ILogger logger = null)
        {
            // Set endpoint
            Endpoint = endpoint;

            // Store callback
            mStateChangedCallback = stateChangedCallback;

            // Log it
            logger?.LogTraceSource($"HttpEndpointChecker started for {endpoint}");

            // Start task
            Task.Run(async () =>
            {
                while (!mDisposing)
                {
                    // Create defaults
                    var webResponse = default(HttpWebResponse);
                    var exception = default(Exception);

                    // Start by calling the endpoint
                    try
                    {
                        // Log it
                        logger?.LogTraceSource($"HttpEndpointChecker fetching {endpoint}");

                        //
                        // By default, presume any response that doesn't throw 
                        // (so the server replied, even if its a 401 for example)
                        // meaning the server we hit up actually responded 
                        // with something even if it was a page not found or server
                        // error. 
                        //
                        // The user is free to override this default behavior
                        //
                        webResponse = await WebRequests.GetAsync(Endpoint);
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }

                    // Figure out the new state
                    //  - If we have a custom parser, ask it for the state based on the response)
                    //  - Otherwise, so long as we have a response of any kind, it's valid
                    var responsive = validResponseParser?.Invoke(webResponse, exception) ?? webResponse != null;

                    // Close the web response
                    webResponse?.Close();

                    // Log it
                    logger?.LogTraceSource($"HttpEndpointChecker {endpoint} { (responsive ? "is" : "is not") } responsive");

                    // If the state has changed...
                    if (responsive != Responsive)
                    {
                        // Set new value
                        Responsive = responsive;

                        // Inform listener
                        mStateChangedCallback?.Invoke(responsive);
                    }

                    // If we are not disposing...
                    if (!mDisposing)
                        // Wait for the interval period
                        await Task.Delay(interval);
                }
            });
        }

        #region Dispose

        /// <summary>
        /// Disposes the task that runs the periodic connectivity check
        /// </summary>
        public void Dispose()
        {
            mDisposing = true;
        }

        #endregion
    }
}

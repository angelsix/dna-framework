using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dna
{
    /// <summary>
    /// Extension methods for <see cref="WaitHandle"/> objects
    /// </summary>
    public static class WaitHandleExtensions
    {
        /// <summary>
        /// Allows awaiting a <see cref="WaitHandle"/>
        /// </summary>
        /// <param name="handle">The handle to await</param>
        /// <param name="millisecondsTimeout">The timeout period to return false if timed out</param>
        /// <param name="cancellationToken">The cancellation token to use to throw a <see cref="TaskCanceledException"/> if this token gets canceled</param>
        /// <returns>Returns true if the handle is free, false if it is not</returns>
        /// <exception cref="TaskCanceledException">Throws if the cancellation token is canceled</exception>
        public static async Task<bool> WaitOneAsync(this WaitHandle handle, int millisecondsTimeout, CancellationToken? cancellationToken)
        {
            // Create a handle that awaiting the original wait handle
            RegisteredWaitHandle registeredWaitHandle = null;

            // Store the token 
            CancellationTokenRegistration? tokenRegistration = null;

            try
            {
                // Create a task completion source to await
                var tcs = new TaskCompletionSource<bool>();

                // Use RegisterWaitForSingleObject so we get a callback
                // once the wait handle has finished, and set the tcs result in that callback
                registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(
                    // The handle to wait on
                    handle, 
                    // When it is finished, set the tcs result
                    (state, timedOut) => ((TaskCompletionSource<bool>)state).TrySetResult(!timedOut),
                    // Pass the tcs as the state so we don't have a reference to the parent tcs (optimization)
                    tcs,
                    // Set timeout if passed in
                    millisecondsTimeout,
                    // Run once don't keep resetting timeout
                    true);

                // Register to run the action and set the tcs as canceled
                // if the cancellation token itself is canceled
                // which will throw a TaskCanceledException up to the caller
                if (cancellationToken.HasValue)
                    tokenRegistration = cancellationToken.Value.Register(state => ((TaskCompletionSource<bool>)state).TrySetCanceled(), tcs);

                // Await the handle or the cancellation token
                return await tcs.Task;
            }
            finally
            {
                // Clean up registered wait handle
                registeredWaitHandle?.Unregister(null);

                // Dispose of the token we had to create to register for the cancellation token callback
                tokenRegistration?.Dispose();
            }
        }

        /// <summary>
        /// Allows awaiting a <see cref="WaitHandle"/>
        /// </summary>
        /// <param name="handle">The handle to await</param>
        /// <param name="timeout">The timeout period to return false if timed out</param>
        /// <param name="cancellationToken">The cancellation token to use to throw a <see cref="TaskCanceledException"/> if this token gets canceled</param>
        /// <returns>Returns true if the handle is free, false if it is not</returns>
        /// <exception cref="TaskCanceledException">Throws if the cancellation token is canceled</exception>
        public static Task<bool> WaitOneAsync(this WaitHandle handle, TimeSpan timeout, CancellationToken? cancellationToken = null)
        {
            return handle.WaitOneAsync((int)timeout.TotalMilliseconds, cancellationToken);
        }

        /// <summary>
        /// Allows awaiting a <see cref="WaitHandle"/>
        /// </summary>
        /// <param name="handle">The handle to await</param>
        /// <param name="cancellationToken">The cancellation token to use to throw a <see cref="TaskCanceledException"/> if this token gets canceled</param>
        /// <returns>Returns true if the handle is free, or waits infinitely until it is free or the cancellation token is canceled</returns>
        /// <exception cref="TaskCanceledException">Throws if the cancellation token is canceled</exception>
        public static Task<bool> WaitOneAsync(this WaitHandle handle, CancellationToken? cancellationToken = null)
        {
            return handle.WaitOneAsync(Timeout.Infinite, cancellationToken);
        }
    }
}

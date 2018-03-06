using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dna
{
    /// <summary>
    /// <para>
    ///     Adds the ability to do the same as lock(...) { } but for async Tasks and awaits
    /// </para>
    /// <para>
    ///     This lock uses the safer semaphore to prevent any chance of a deadlock
    /// </para>
    /// </summary>
    /// <example>
    /// <code>
    ///     await AsyncLock.LockAsync("myKey", () => DoSomething());
    /// </code>
    /// </example>
    public static class AsyncLock
    {
        #region Private Members

        /// <summary>
        /// A semaphore to lock the semaphore list
        /// </summary>
        private static SemaphoreSlim SelfLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// A list of all semaphore locks (one per key)
        /// </summary>
        private static Dictionary<string, SemaphoreDetails> Semaphores = new Dictionary<string, SemaphoreDetails>();

        #endregion

        /// <summary>
        /// Awaits for any outstanding tasks to complete that are accessing the same key then runs the given task, returning it's value
        /// </summary>
        /// <param name="key">The key to await</param>
        /// <param name="task">The task to perform inside of the semaphore lock</param>
        /// <param name="maxAccessCount">If this is the first call, sets the maximum number of tasks that can access this task before it waiting</param>
        /// <returns>Returns the result of the task</returns>
        public static async Task<T> LockResultAsync<T>(string key, Func<Task<T>> task, int maxAccessCount = 1)
        {
            #region Create Semaphore

            //
            // Asynchronously wait to enter the Semaphore
            //
            //      If no-one has been granted access to the Semaphore
            //      code execution will proceed
            //
            //      Otherwise this thread waits here until the semaphore is released 
            //
            await SelfLock.WaitAsync();

            try
            {
                // If the semaphore with this key does not exist...
                if (!Semaphores.ContainsKey(key))
                    // Create it
                    Semaphores.Add(key, new SemaphoreDetails(key, maxAccessCount));
            }
            finally
            {
                //
                // When the task is ready, release the semaphore
                //
                //      It is vital to ALWAYS release the semaphore when we are ready
                //      or else we will end up with a Semaphore that is forever locked
                //      This is why it is important to do the Release within a try...finally clause
                //      Program execution may crash or take a different path, this way you are guaranteed execution
                //
                SelfLock.Release();
            }

            #endregion

            // Now use this semaphore and perform the desired task inside its lock
            // NOTE: We never remove semaphores after creating them, so this will never be null
            var semaphore = Semaphores[key];

            // Await this semaphore
            await semaphore.Semaphore.WaitAsync();

            try
            {
                // Perform the job
                return await task();
            }
            finally
            {
                // Release the semaphore
                semaphore.Semaphore.Release();
            }
        }

        /// <summary>
        /// Awaits for any outstanding tasks to complete that are accessing the same key then runs the given task
        /// </summary>
        /// <param name="key">The key to await</param>
        /// <param name="task">The task to perform inside of the semaphore lock</param>
        /// <param name="maxAccessCount">If this is the first call, sets the maximum number of tasks that can access this task before it waiting</param>
        /// <returns></returns>
        public static async Task LockAsync(string key, Func<Task> task, int maxAccessCount = 1)
        {
            // Run the result async lock just returning true
            // so that we share the same logic just ignoring the return value
            await LockResultAsync(key, async () =>
            {
                // Run the task
                await task();

                // Send pointless result that we ignore
                return true;

            }, maxAccessCount);
        }

        /// <summary>
        /// Awaits for any outstanding tasks to complete that are accessing the same key then runs the given action
        /// </summary>
        /// <param name="key">The key to await</param>
        /// <param name="task">The task to perform inside of the semaphore lock</param>
        /// <param name="maxAccessCount">If this is the first call, sets the maximum number of tasks that can access this task before it waiting</param>
        /// <returns></returns>
        public static Task LockAsync(string key, Action task, int maxAccessCount = 1)
        {
            // Run the result async lock just returning true
            // so that we share the same logic just ignoring the return value
            return LockResultAsync(key, () =>
            {
                // Run the task
                task();

                // Send pointless result that we ignore
                return Task.FromResult(true);

            }, maxAccessCount);
        }

        /// <summary>
        /// Awaits for any outstanding tasks to complete that are accessing the same key then runs the given task, returning it's value
        /// </summary>
        /// <param name="key">The key to await</param>
        /// <param name="task">The task to perform inside of the semaphore lock</param>
        /// <param name="maxAccessCount">If this is the first call, sets the maximum number of tasks that can access this task before it waiting</param>
        /// <returns>Returns the result of the task</returns>
        public static Task<T> LockResultAsync<T>(string key, Func<T> task, int maxAccessCount = 1)
        {
            // Run the result async lock just returning true
            // so that we share the same logic just ignoring the return value
            return LockResultAsync(key, () =>
            {
                // Run the function and return the result as a task
                return Task.FromResult(task());

            }, maxAccessCount);
        }
    }
}
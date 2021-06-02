using System;
using System.Threading.Tasks;

namespace Asynq
{
    public static class AsynqTasks
    {
        /// <summary>
        /// Returns all values from each ValueTask as an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueTasks"></param>
        /// <returns>Array of values when all ValueTasks have been completed</returns>
        public static ValueTask<T[]> WhenAll<T>(params ValueTask<T>[] valueTasks)
        {
            return valueTasks.WhenAll();
        }

        /// <summary>
        /// Returns any of the completed ValueTasks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueTasks"></param>
        /// <returns>One of the completed ValueTasks</returns>
        public static ValueTask<T> WhenAny<T>(params ValueTask<T>[] valueTasks)
        {
            return valueTasks.WhenAny();
        }

        /// <summary>
        /// Fire and Forget a ValueTask object.
        /// This method will not throw any exceptions from the execution of the value task.
        ///
        /// In case an exception is thrown or the task fails to complete onException handler will be called with the exception details.
        /// </summary>
        /// <param name="valueTask"></param>
        /// <param name="onException"></param>
        public static async void FireAndForget(this ValueTask valueTask, Action<Exception> onException = null)
        {
            try
            {
                await valueTask.ConfigureAwait(false);
            }
            catch (Exception e) when (onException != null)
            {
                onException(e);
            }
        }
    }
}
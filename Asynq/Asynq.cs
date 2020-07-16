using System;
using System.Threading.Tasks;

namespace Asynq
{
    public static class Asynq
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
        ///
        /// </summary>
        /// <param name="valueTask"></param>
        /// <param name="onException"></param>
        public static async void FireAndForget(this ValueTask valueTask, Action<Exception> onException = null)
        {
            try
            {
                await valueTask.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (onException != null)
                {
                    onException(e);
                }
            }
        }
    }
}
using System;
using System.Threading.Tasks;

namespace Asynq
{
    public static class TaskExtensions
    {
        public static ValueTask AsValueTask(this Task task)
        {
            return new ValueTask(task);
        }

        public static ValueTask<T> AsValueTask<T>(this Task<T> task)
        {
            return new ValueTask<T>(task);
        }

        /// <summary>
        /// Fire and Forget a Task object.
        /// This method will not throw any exceptions from the execution of the value task.
        ///
        /// In case an exception is thrown or the task fails to complete onException handler will be called with the exception details.
        /// </summary>
        /// <param name="valueTask"></param>
        /// <param name="onException"></param>
        public static async void FireAndForget(this Task task, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception e) when (onException != null)
            {
                onException(e);
            }
        }
    }
}
using System;
using System.Threading.Tasks;

namespace Asynq
{
    public static class Asynq
    {
        public static ValueTask<T[]> WhenAll<T>(params ValueTask<T>[] valueTasks)
        {
            return valueTasks.WhenAll();
        }

        public static ValueTask<T> WhenAny<T>(params ValueTask<T>[] valueTasks)
        {
            return valueTasks.WhenAny();
        }

        public static async void FireAndForget(this ValueTask valueTask, Action<Exception> onException = null)
        {
            try
            {
                await valueTask.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (onException == null)
                {
                    throw;
                }

                onException(e);
            }
        }
    }
}
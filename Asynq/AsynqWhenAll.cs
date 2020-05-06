using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asynq
{
    public static class AsynqWhenAll
    {
        public static async ValueTask<T[]> WhenAll<T>(this ValueTask<T>[] valueTasks)
        {
            T[] values = new T[valueTasks.Length];
            List<Task<T>> toAwait = null;

            for (int i = 0; i < values.Length; i++)
            {
                if (valueTasks[i].IsCompletedSuccessfully)
                {
                    values[i] = valueTasks[i].Result;
                }
                else
                {
                    toAwait ??= new List<Task<T>>(valueTasks.Length);
                    toAwait.Add(valueTasks[i].AsTask());
                }
            }

            if (toAwait == null)
            {
                return values;
            }

            var awaited = await Task.WhenAll(toAwait);
            int key = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != null)
                {
                    continue;
                }

                values[i] = awaited[key];
                key++;
            }

            return values;
        }
    }
}
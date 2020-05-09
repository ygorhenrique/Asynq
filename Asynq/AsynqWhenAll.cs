using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asynq
{
    public static class AsynqWhenAll
    {
        public static async ValueTask<T[]> WhenAll<T>(this ValueTask<T>[] valueTasks)
        {
            T[] values = new T[valueTasks.Length];
            Dictionary<int, Task<T>> toAwait = null;

            for (int i = 0; i < values.Length; i++)
            {
                if (valueTasks[i].IsCompletedSuccessfully)
                {
                    values[i] = valueTasks[i].Result;
                }
                else
                {
                    toAwait ??= new Dictionary<int, Task<T>>(valueTasks.Length);
                    toAwait.Add(i, valueTasks[i].AsTask());
                }
            }

            if (toAwait == null)
            {
                return values;
            }

            var awaited = await Task.WhenAll(toAwait.Values);
            foreach (var key in toAwait.Keys)
            {
                values[key] = toAwait[key].Result;
            }

            return values;
        }
    }
}
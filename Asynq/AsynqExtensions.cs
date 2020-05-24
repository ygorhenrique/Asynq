using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asynq
{
    public static class AsynqExtensions
    {
        public static async ValueTask<T[]> WhenAll<T>(this ValueTask<T>[] valueTasks)
        {
            if (valueTasks.Length == 0)
                return Array.Empty<T>();

            T[] values
                = new T[valueTasks.Length];

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

            await Task.WhenAll(toAwait.Values)
                .ConfigureAwait(false);

            foreach (var key in toAwait.Keys)
            {
                values[key] = toAwait[key].Result;
            }

            return values;
        }

        public static async ValueTask WhenAll(this ValueTask[] valueTasks)
        {
            if (valueTasks.Length == 0)
                return;

            Task[] toAwait =
                new Task[valueTasks.Length];

            int completedTasks = 0;

            for (int i = 0; i < valueTasks.Length; i++)
            {
                if (!valueTasks[i].IsCompletedSuccessfully)
                {
                    toAwait[i] = valueTasks[i].AsTask();
                }
                else
                {
                    completedTasks++;
                }
            }
            if (completedTasks != valueTasks.Length)
                await Task.WhenAll(toAwait)
                    .ConfigureAwait(false);
        }

        public static async ValueTask<T> WhenAny<T>(this ValueTask<T>[] valueTasks)
        {
            if (valueTasks == null)
                throw new ArgumentNullException(nameof(valueTasks));

            if (valueTasks.Length == 0)
                throw new ArgumentException("Array was empty.", nameof(valueTasks));

            for (int i = 0; i < valueTasks.Length; i++)
            {
                if (valueTasks[i].IsCompletedSuccessfully)
                {
                    return valueTasks[i].Result;
                }
            }

            Task<T>[] tasks
                = new Task<T>[valueTasks.Length];

            for (int i = 0; i < valueTasks.Length; i++)
            {
                tasks[i] = valueTasks[i].AsTask();
            }

            return (await Task.WhenAny(tasks)
                .ConfigureAwait(false)).Result;
        }

        public static async ValueTask<ValueTask> WhenAny(this ValueTask[] valueTasks)
        {
            if (valueTasks == null)
                throw new ArgumentNullException(nameof(valueTasks));

            if (valueTasks.Length == 0)
                throw new ArgumentException("Array was empty.", nameof(valueTasks));

            Task[] toAwait =
                new Task[valueTasks.Length];

            for (int i = 0; i < valueTasks.Length; i++)
            {
                if (valueTasks[i].IsCompletedSuccessfully)
                {
                    return valueTasks[i];
                }
                else
                {
                    toAwait[i] = valueTasks[i].AsTask();
                }
            }

            return new ValueTask(await Task.WhenAny(toAwait)
                .ConfigureAwait(false));
        }
    }
}
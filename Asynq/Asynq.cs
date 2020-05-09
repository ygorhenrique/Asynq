using System.Threading.Tasks;

namespace Asynq
{
    public static class Asynq
    {
        public static ValueTask<T[]> WhenAll<T>(ValueTask<T>[] valueTasks)
        {
            return valueTasks.WhenAll();
        }
    }
}
using System.Threading.Tasks;

namespace Asynq
{
    public static class Asynq
    {
        public static ValueTask<T[]> WhenAll<T>(params ValueTask<T>[] valueTasks)
        {
            return valueTasks.WhenAll();
        }
    }
}
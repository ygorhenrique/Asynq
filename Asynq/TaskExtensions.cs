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
    }
}
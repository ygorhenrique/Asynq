using System.Threading.Tasks;
using Xunit;

namespace Asynq.Tests
{
    public class TaskExtensionsTest
    {
        [Fact]
        public async Task GenericAsTask_ValueTaskHoldsOriginalTask()
        {
            Task<int> asyncVal = AsyncValue(100);

            ValueTask<int> valueTask = asyncVal.AsValueTask();

            Assert.Equal(asyncVal, valueTask.AsTask());
            Assert.False(valueTask.IsCompletedSuccessfully);
            Assert.Equal(100, await valueTask);
        }

        [Fact]
        public async Task NonGenericAsTask_ReturnsValidValueTask()
        {
            Task asyncTask = Task.Delay(10);
            ValueTask valueTask = asyncTask.AsValueTask();

            Assert.Equal(asyncTask, valueTask.AsTask());
            Assert.False(valueTask.IsCompletedSuccessfully);
            await valueTask;
            Assert.True(valueTask.IsCompletedSuccessfully);
        }

        public async Task<T> AsyncValue<T>(T value)
        {
            await Task.Delay(10);
            return value;
        }
    }
}
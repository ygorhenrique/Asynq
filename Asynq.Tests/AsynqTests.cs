using System.Threading.Tasks;
using Xunit;

namespace Asynq.Tests
{
    public class AsynqTests
    {
        [Fact]
        public async Task AllValueTasksCompleted()
        {
            ValueTask<int>[] tasks = new[]
            {
                new ValueTask<int>(0),
                new ValueTask<int>(1),
                new ValueTask<int>(2),
                new ValueTask<int>(3)
            };

            var values = await Asynq.WhenAll(tasks);

            Assert.Collection(values,
                value => Assert.Equal(0, value),
                value => Assert.Equal(1, value),
                value => Assert.Equal(2, value),
                value => Assert.Equal(3, value));
        }

        [Fact]
        public async Task AllValueTasksIncompleted()
        {
            var values = await Asynq.WhenAll(new ValueTask<int>(DelayedInt(0)),
                new ValueTask<int>(DelayedInt(1)),
                new ValueTask<int>(DelayedInt(2)),
                new ValueTask<int>(DelayedInt(3)));

            Assert.Collection(values,
                value => Assert.Equal(0, value),
                value => Assert.Equal(1, value),
                value => Assert.Equal(2, value),
                value => Assert.Equal(3, value));
        }

        private async Task<int> DelayedInt(int value)
        {
            await Task.Delay(1000);
            return value;
        }
    }
}
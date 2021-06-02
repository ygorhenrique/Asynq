using System;
using System.Threading.Tasks;
using Xunit;
using Asynq;

namespace AsynqTests
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

            var values = await AsynqTasks.WhenAll(tasks);

            Assert.Collection(values,
                value => Assert.Equal(0, value),
                value => Assert.Equal(1, value),
                value => Assert.Equal(2, value),
                value => Assert.Equal(3, value));
        }

        [Fact]
        public async Task AllValueTasksIncompleted()
        {
            var values = await AsynqTasks.WhenAll(DelayedInt(0),
                DelayedInt(1),
                DelayedInt(2),
                DelayedInt(3));

            Assert.Collection(values,
                value => Assert.Equal(0, value),
                value => Assert.Equal(1, value),
                value => Assert.Equal(2, value),
                value => Assert.Equal(3, value));
        }

        [Fact]
        public async Task WhenAny_AllValueTasksCompleted()
        {
            ValueTask<int>[] tasks = {
                new ValueTask<int>(3),
                new ValueTask<int>(2),
                new ValueTask<int>(1),
                new ValueTask<int>(0)
            };

            Assert.Equal(3, await tasks.WhenAny());
        }

        [Fact]
        public async Task WhenAny_AllValueTasksIncompleted()
        {
            ValueTask<int>[] tasks = {
                DelayedInt(3, 1000),
                DelayedInt(2, 1000),
                DelayedInt(1, 500),
                DelayedInt(0, 1000)
            };

            Assert.Equal(1, await AsynqTasks.WhenAny(tasks));
        }

        [Fact]
        public async Task WhenAny_ArgumentExceptionThrownWhenEmptyArrayOfValueTask()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await AsynqTasks.WhenAny(Array.Empty<ValueTask<int>>()));
        }

        private async ValueTask<int> DelayedInt(int value, int delay = 1000)
        {
            await Task.Delay(delay);
            return value;
        }
    }
}
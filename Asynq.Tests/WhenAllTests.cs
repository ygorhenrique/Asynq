using System;
using System.Threading.Tasks;
using Xunit;

namespace Asynq.Tests
{
    public class WhenAllTests
    {
        [Fact]
        public async Task GenericsWhenAll_AllValueTasksCompleted()
        {
            ValueTask<int>[] tasks = {
                new ValueTask<int>(0),
                new ValueTask<int>(1),
                new ValueTask<int>(2),
                new ValueTask<int>(3)
            };

            var values = await tasks.WhenAll();

            Assert.Collection(values,
                value => Assert.Equal(0, value),
                value => Assert.Equal(1, value),
                value => Assert.Equal(2, value),
                value => Assert.Equal(3, value));
        }

        [Fact]
        public async Task GenericsWhenAll_AllValueTasksIncompleted()
        {
            ValueTask<int>[] tasks = {
                DelayedInt(0),
                DelayedInt(1),
                DelayedInt(2),
                DelayedInt(3)
            };

            var values = await tasks.WhenAll();

            Assert.Collection(values,
                value => Assert.Equal(0, value),
                value => Assert.Equal(1, value),
                value => Assert.Equal(2, value),
                value => Assert.Equal(3, value));
        }

        [Fact]
        public async Task GenericsWhenAll_EmptyArrayOfValueTasksReturnsEmptyArrayOfValues()
        {
            Assert.Empty(await Array.Empty<ValueTask<int>>().WhenAll());
        }

        [Fact]
        public async Task AllValueTasksCompleted()
        {
            ValueTask[] tasks = {
                new ValueTask(),
                new ValueTask(),
                new ValueTask(),
                new ValueTask()
            };

            await tasks.WhenAll();

            Assert.Collection(tasks, value => Assert.True(value.IsCompletedSuccessfully),
                value => Assert.True(value.IsCompletedSuccessfully),
                value => Assert.True(value.IsCompletedSuccessfully),
                value => Assert.True(value.IsCompletedSuccessfully));
        }

        [Fact]
        public async Task AllValueTasksIncompleted()
        {
            ValueTask[] tasks = {
                new ValueTask(Task.Delay(100)),
                new ValueTask(Task.Delay(100)),
                new ValueTask(Task.Delay(100)),
                new ValueTask(Task.Delay(100)),
            };

            await tasks.WhenAll();

            Assert.Collection(tasks, value => Assert.True(value.IsCompletedSuccessfully),
                value => Assert.True(value.IsCompletedSuccessfully),
                value => Assert.True(value.IsCompletedSuccessfully),
                value => Assert.True(value.IsCompletedSuccessfully));
        }

        [Fact]
        public async Task EmptyArrayOfValueTasksReturnsEmptyArrayOfValues()
        {
            Assert.Empty(await Array.Empty<ValueTask<int>>().WhenAll());
        }

        private async ValueTask<int> DelayedInt(int value)
        {
            await Task.Delay(100);
            return value;
        }
    }
}
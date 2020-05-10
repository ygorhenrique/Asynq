using System;
using System.Threading.Tasks;
using Xunit;

namespace Asynq.Tests
{
    public class WhenAnyTests
    {
        [Fact]
        public async Task Generics_AllValueTasksCompleted()
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
        public async Task Generics_AllValueTasksIncompleted()
        {
            ValueTask<int>[] tasks = {
                DelayedInt(3, 1000),
                DelayedInt(2, 1000),
                DelayedInt(1, 500),
                DelayedInt(0, 1000)
            };

            Assert.Equal(1, await tasks.WhenAny());
        }

        [Fact]
        public async Task Generics_ArgumentExceptionThrownWhenEmptyArrayOfValueTask()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await Array.Empty<ValueTask<int>>().WhenAny());
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

            Assert.Equal(tasks[0], await tasks.WhenAny());
        }

        [Fact]
        public async Task AllValueTasksIncompleted()
        {
            ValueTask[] tasks = {
                new ValueTask(Task.Delay(100)),
                new ValueTask(Task.Delay(100)),
                new ValueTask(Task.Delay(50)),
                new ValueTask(Task.Delay(100)),
            };

            Assert.Equal(tasks[2], await tasks.WhenAny());
        }

        [Fact]
        public async Task ArgumentExceptionThrownWhenEmptyArrayOfValueTask()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await Array.Empty<ValueTask>().WhenAny());
        }

        private async ValueTask<int> DelayedInt(int value, int delay)
        {
            await Task.Delay(delay);
            return value;
        }
    }
}
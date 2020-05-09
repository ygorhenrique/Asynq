using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Asynq.Tests
{
    public class AsynqTests
    {
        [Fact]
        public async Task AllValueTasksCompleted()
        {
            ValueTask<int>[] tasks = new ValueTask<int>[]
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
    }
}
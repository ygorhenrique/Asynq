using System.Threading.Tasks;
using Xunit;

namespace Asynq.Tests
{
    public class AsynqWhenAllTests
    {
        [Fact]
        public async Task Test1()
        {
            ValueTask<int>[] tasks = new ValueTask<int>[] { FromInt(0), FromInt(1), FromInt(2), FromInt(3) };

            var values = await tasks.WhenAll();

            Assert.Collection(values,
                value => Assert.Equal(0, value),
                value => Assert.Equal(1, value),
                value => Assert.Equal(2, value),
                value => Assert.Equal(3, value));
        }

        public ValueTask<int> FromInt(int result)
        {
            return new ValueTask<int>(result);
        }
    }
}
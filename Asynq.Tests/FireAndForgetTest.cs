using System.Threading.Tasks;
using Xunit;

namespace Asynq.Tests
{
    public class FireAndForgetTest
    {
        [Fact]
        public async Task FireAndForget()
        {
            Task asyncTask = Task.Delay(1000);
            ValueTask valueTask = new ValueTask(asyncTask);

            valueTask.FireAndForget();

            Assert.False(asyncTask.IsCompletedSuccessfully);
            await asyncTask;
            Assert.True(asyncTask.IsCompletedSuccessfully);
        }
    }
}
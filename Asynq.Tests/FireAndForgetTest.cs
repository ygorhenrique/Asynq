using System;
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

        [Fact]
        public async Task FireAndForget_ShouldNotThrowAnyExceptions()
        {
            Task asyncTask = Task.Delay(1000);

            asyncTask.FireAndForget();

            Assert.False(asyncTask.IsCompletedSuccessfully);
            await asyncTask;
            Assert.True(asyncTask.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task FireAndForgetValueTask_ShouldCallDelegateWhenDelegateIsSpecified()
        {
            Task taskWithException = Task.FromException<Exception>(new Exception("Should throw an exception"));

            taskWithException.FireAndForget(onException: (ex) =>
            {
                Assert.NotNull(ex);
                Assert.Equal("Should throw an exception", ex.Message);
            });
        }

        [Fact]
        public async Task FireAndForgetTask_ShouldCallDelegateWhenDelegateIsSpecified()
        {
            Task taskWithException = Task.FromException<Exception>(new Exception("Should throw an exception"));
            ValueTask valueTask = new ValueTask(taskWithException);

            valueTask.FireAndForget(onException: (ex) =>
            {
                Assert.NotNull(ex);
                Assert.Equal("Should throw an exception", ex.Message);
            });
        }
    }
}
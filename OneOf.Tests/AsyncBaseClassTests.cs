using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.Tests
{
    public class AsyncBaseClassTests
    {
        [Test]
        public async Task CanAsyncMatchOnBase()
        {
            var x = await SimulateAsyncCall().ConfigureAwait(false);

            var result = await x.MatchAsync(
                async methodNotAllowed => await SimulateReadResponseAsync(methodNotAllowed).ConfigureAwait(false),
                async invokeSuccessResponse => await SimulateReadResponseAsync(invokeSuccessResponse).ConfigureAwait(false))
                .ConfigureAwait(false);

            Assert.AreEqual(true, result);
        }

        private Task<Response> SimulateAsyncCall()
        {
            Response response = new Response.InvokeSuccessResponse();
            return Task.FromResult(response);
        }

        private Task<bool> SimulateReadResponseAsync(Response.InvokeSuccessResponse success) => Task.FromResult(true);
        private Task<bool> SimulateReadResponseAsync(Response.MethodNotAllowed notAllowed) => Task.FromResult(false);
    }
}

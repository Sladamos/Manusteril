using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Middleware
{
    internal class MiddlewaresWrapper
    {
        private readonly List<IMiddleware> middlewares;

        public MiddlewaresWrapper(List<IMiddleware> middlewares) => this.middlewares = middlewares;

        public async Task execute(Func<Task> task)
        {
            Func<Task> pipeline = middlewares.AsEnumerable().Reverse().Aggregate(task, (next, middleware) => async () => await middleware.Invoke(next));
            await pipeline();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Middleware
{
    internal class MiddlewaresWrapper 
    {
        private readonly List<IMiddleware> middlewares;

        public MiddlewaresWrapper(List<IMiddleware> middlewares) => this.middlewares = middlewares;

        public void execute(Action task)
        {
            Action pipeline = middlewares.AsEnumerable().Reverse().Aggregate(task, (next, middleware) => () => middleware.Invoke(next));
            pipeline();
        }

    }
}

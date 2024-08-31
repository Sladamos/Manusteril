using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Middleware
{
    internal interface IMiddleware
    {
        Task Invoke(Func<Task> task);
    }
}

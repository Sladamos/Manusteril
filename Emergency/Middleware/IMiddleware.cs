using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Middleware
{
    internal interface IMiddleware
    {
        void Invoke(Action task);
    }
}

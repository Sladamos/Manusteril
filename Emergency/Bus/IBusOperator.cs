using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Bus
{
    internal interface IBusOperator
    {
        IBusClient<TRequest> CreateBusClient<TRequest>() where TRequest : class;
        IBusInstance CreateBusInstance();
    }
}

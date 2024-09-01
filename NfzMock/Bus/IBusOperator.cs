using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Bus
{
    internal interface IBusOperator
    {
        IBusClient<TRequest> CreateBusClient<TRequest>() where TRequest : class;
        void RegisterConsumer<TMessage>(IBusConsumer<TMessage> consumer) where TMessage : class;
        IBusInstance CreateBusInstance();
    }
}

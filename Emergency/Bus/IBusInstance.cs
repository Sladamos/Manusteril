using Emergency.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Bus
{
    internal interface IBusInstance
    {
        IBusClient<TRequest> GetClient<TRequest>() where TRequest : class;
        Task Publish(object message);
        Task Start();
        Task Stop();
        void ConnectConsumer<TMessage>(IBusConsumer<TMessage> consumer) where TMessage : class;
    }
}

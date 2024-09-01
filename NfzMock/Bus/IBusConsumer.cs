using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Bus
{

    internal interface IBusConsumer<TMessage> : IConsumer<TMessage> where TMessage : class
    {
        string QueueName { get; }
    }
}

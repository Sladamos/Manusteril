using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Bus;

namespace Ward.Handler
{
    internal class NewPatientRegisteredHandler : IBusConsumer<INewPatientRegisteredMessage>
    {
        public string QueueName => throw new NotImplementedException();

        public Task Consume(ConsumeContext<INewPatientRegisteredMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}

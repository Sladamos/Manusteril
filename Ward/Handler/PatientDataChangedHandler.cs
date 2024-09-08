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
    internal class PatientDataChangedHandler : IBusConsumer<IPatientDataChangedMessage>
    {
        public string QueueName => throw new NotImplementedException();

        public Task Consume(ConsumeContext<IPatientDataChangedMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}

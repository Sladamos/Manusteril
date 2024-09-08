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
    internal class PatientVisitRegisteredHandler : IBusConsumer<IPatientVisitRegisteredMessage>
    {
        public string QueueName => throw new NotImplementedException();

        public Task Consume(ConsumeContext<IPatientVisitRegisteredMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}

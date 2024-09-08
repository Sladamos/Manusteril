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
    internal class PatientVisitAcceptedMessageHandler : IBusConsumer<IPatientVisitAcceptedMessage>
    {
        public string QueueName => throw new NotImplementedException();

        public Task Consume(ConsumeContext<IPatientVisitAcceptedMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}

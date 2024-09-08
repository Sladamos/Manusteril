using log4net;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Bus;
using Ward.Visit;

namespace Ward.Handler
{
    internal class PatientVisitUnregisteredHandler : IBusConsumer<IPatientVisitUnregisteredMessage>
    {
        private ILog logger;

        private IVisitService visitService;

        private string wardIdentifier;

        public PatientVisitUnregisteredHandler(ILog logger, IVisitService visitService)
        {
            this.logger = logger;
            this.visitService = visitService;
        }

        public string QueueName => $"{wardIdentifier}_visitUnregistered";

        public async Task Consume(ConsumeContext<IPatientVisitUnregisteredMessage> context)
        {
            try
            {
                var message = context.Message;
                visitService.GetPatientCurrentVisit(message.PatientPesel);
                logger.Info($"Otrzymano potwierdzenie opuszczenia: {message}");
                visitService.MarkVisitAsFinished(message);
            }
            catch (UnregisteredPatientException)
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze potwierdzenia: {ex.Message}");
            }
        }
    }
}

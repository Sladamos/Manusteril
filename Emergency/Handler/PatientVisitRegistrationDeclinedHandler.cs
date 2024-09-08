using Emergency.Bus;
using Emergency.Visit;
using log4net;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Handler
{
    internal class PatientVisitRegistrationDeclinedHandler : IBusConsumer<IPatientVisitRegistrationDeclinedResponse>
    {
        private ILog logger;

        public PatientVisitRegistrationDeclinedHandler(ILog logger)
        {
            this.logger = logger;
        }

        public string QueueName => "emergency_visitDeclined";

        public async Task Consume(ConsumeContext<IPatientVisitRegistrationDeclinedResponse> context)
        {
            try
            {
                var message = context.Message;
                logger.Info($"Otrzymano odmowę przyjęcia: {message}");
                Console.WriteLine($"Oddział {message.WardIdentifier} informuje, że NIE może przyjąć pacjenta {message.PatientPesel}, z powodu: {message.Reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze akceptacji przyjęcia: {ex.Message}");
                throw;
            }
        }
    }
}

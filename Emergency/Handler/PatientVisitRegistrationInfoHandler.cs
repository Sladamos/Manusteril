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
    internal class PatientVisitRegistrationInfoHandler : IBusConsumer<IPatientVisitRegistrationAcceptedResponse>, IBusConsumer<IPatientVisitRegistrationDeclinedResponse>
    {
        private ILog logger;

        public PatientVisitRegistrationInfoHandler(ILog logger)
        {
            this.logger = logger;
        }

        public string QueueName => "emergency_visitInfo";

        public async Task Consume(ConsumeContext<IPatientVisitRegistrationAcceptedResponse> context)
        {
            try
            {
                var message = context.Message;
                logger.Info($"Otrzymano akceptację przyjęcia: {message}");
                Console.WriteLine($"Oddział {message.WardIdentifier} informuje, że może przyjąć pacjenta {message.PatientPesel}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze akceptacji przyjęcia: {ex.Message}");
                throw;
            }
        }

        public async Task Consume(ConsumeContext<IPatientVisitRegistrationDeclinedResponse> context)
        {
            try
            {
                var message = context.Message;
                logger.Info($"Otrzymano odmowę przyjęcia: {message}");
                Console.WriteLine($"Oddział {message.WardIdentifier} informuje, że NIE może przyjąć pacjenta {message.PatientPesel}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze akceptacji przyjęcia: {ex.Message}");
                throw;
            }
        }
    }
}

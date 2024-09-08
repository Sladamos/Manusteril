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
    internal class PatientAllowedToLeaveHandler : IBusConsumer<IPatientAllowedToLeaveMessage>
    {
        private ILog logger;

        private IVisitService visitService;

        public PatientAllowedToLeaveHandler(ILog logger, IVisitService visitService)
        {
            this.logger = logger;
            this.visitService = visitService;
        }

        public string QueueName => "emergency_allowedToLeave";

        public async Task Consume(ConsumeContext<IPatientAllowedToLeaveMessage> context)
        {
            try
            {
                var message = context.Message;
                logger.Info($"Otrzymano pozwolenie na opuszczenie: {message}");
                visitService.MarkVisitAsFinished(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze pozwolenia: {ex.Message}");
            }
        }
    }
}

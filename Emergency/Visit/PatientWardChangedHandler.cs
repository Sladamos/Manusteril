using Emergency.Bus;
using log4net;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal class PatientWardChangedHandler : IBusConsumer<IPatientWardChanged>
    {
        private ILog logger;

        private IVisitService visitService;

        public PatientWardChangedHandler(ILog logger, IVisitService visitService)
        {
            this.logger = logger;
            this.visitService = visitService;
        }

        public string QueueName => "emergency_changeWard";

        public async Task Consume(ConsumeContext<IPatientWardChanged> context)
        {
            try
            {
                var message = context.Message;
                logger.Info($"Otrzymano zmianę oddziału: {message}");
                visitService.ChangePatientWard(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze zmiany oddziału: {ex.Message}");
                throw;
            }
        }
    }
}

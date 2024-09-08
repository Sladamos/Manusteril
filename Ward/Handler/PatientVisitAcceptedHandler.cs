using log4net;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Bus;
using Ward.Config;
using Ward.Visit;

namespace Ward.Handler
{
    internal class PatientVisitAcceptedHandler : IBusConsumer<IPatientVisitAcceptedMessage>
    {
        private ILog logger;

        private IVisitService visitService;

        private string wardIdentifier;

        public PatientVisitAcceptedHandler(ILog logger, IVisitService visitService, WardConfig wardConfig)
        {
            this.logger = logger;
            this.visitService = visitService;
            wardIdentifier = wardConfig.WardIdentifier;
        }

        public string QueueName => $"{wardIdentifier}_visitAccepted";

        public async Task Consume(ConsumeContext<IPatientVisitAcceptedMessage> context)
        {
            try
            {
                var message = context.Message;
                if(wardIdentifier == message.WardIdentifier)
                {
                    var visit = new VisitEntity
                    {
                        Id = message.VisitId,
                        PatientId = message.PatientId,
                        PatientPesel = message.PatientPesel,
                        VisitStartDate = message.VisitStartDate,
                        AllowedToLeave = false,
                        PatientRoomNumber = "",
                        VisitState = Messages.VisitEntityState.NEW
                    };
                    visitService.AddVisit(visit);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze potwierdzenia: {ex.Message}");
                throw;
            }
        }
    }
}

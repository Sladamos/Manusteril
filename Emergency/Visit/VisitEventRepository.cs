using Emergency.Bus;
using Emergency.Message;
using Emergency.Messages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal class VisitEventRepository : IVisitEventRepository
    {
        private IBusInstance busInstance;

        private ILog logger;

        public VisitEventRepository(IBusOperator busOperator, ILog logger)
        {
            busInstance = busOperator.CreateBusInstance();
            this.logger = logger;
        }

        public void AskForRegistration(VisitEntity visit)
        {
            PatientVisitRegisteredMessage message = new()
            {
                PatientId = visit.PatientId,
                PatientPesel = visit.PatientPesel,
                VisitId = visit.Id,
                WardType = visit.Ward
            };
            logger.Info($"Wysłanie zapytania o rejestracje: {visit}");
            busInstance.Publish(message);
        }

        public void Register(VisitEntity visit)
        {
            PatientVisitAcceptedMessage message = new () { PatientId = visit.PatientId,
                PatientPesel = visit.PatientPesel,
                VisitId = visit.Id,
                VisitStartDate = visit.VisitStartDate,
                WardIdentifier = visit.WardIdentifier
            };
            logger.Info($"Wysłanie wiadomości o zarejestrowaniu: {visit}");
            busInstance.Publish(message);
        }

        public void Unregister(VisitEntity visit)
        {
            PatientVisitUnregisteredMessage message = new() { PatientId = visit.PatientId,
                PatientPesel = visit.PatientPesel,
                VisitId = visit.Id,
                VisitEndDate = visit.VisitEndDate ?? DateTime.Now
            };
            logger.Info($"Wysłanie wiadomości o wyrejestrowaniu: {visit}");
            busInstance.Publish(message);
        }
    }
}

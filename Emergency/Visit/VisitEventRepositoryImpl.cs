using Emergency.Bus;
using Emergency.Messages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal class VisitEventRepositoryImpl : IVisitEventRepository
    {
        private IBusInstance busInstance;

        private ILog logger;

        public VisitEventRepositoryImpl(IBusOperator busOperator, ILog logger)
        {
            busInstance = busOperator.CreateBusInstance();
            this.logger = logger;
        }

        public void Register(VisitEntity visit)
        {
            PatientVisitRegisteredMessage message = new () { PatientId = visit.PatientId,
                PatientPesel = visit.PatientPesel,
                VisitId = visit.Id,
                WardType = visit.Ward};
            logger.Info($"Wysłanie wiadomości o zarejestrowaniu: {message}");
            busInstance.Publish(message);
        }

        public void Unregister(VisitEntity visit)
        {
            PatientVisitUnregisteredMessage message = new() { PatientId = visit.PatientId,
                PatientPesel = visit.PatientPesel,
                VisitId = visit.Id};
            logger.Info($"Wysłanie wiadomości o wyrejestrowaniu: {message}");
            busInstance.Publish(message);
        }
    }
}

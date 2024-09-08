using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Bus;

namespace Ward.Handler
{
    internal class ConsumersWrapper
    {
        private IBusConsumer<INewPatientRegisteredMessage> newPatientRegisteredConumser;
        private IBusConsumer<IPatientDataChangedMessage> patientDataChangedConsumer;
        private IBusConsumer<IPatientVisitAcceptedMessage> patientVisitAcceptedConsumer;
        private IBusConsumer<IPatientVisitRegisteredMessage> patientVisitRegisteredConsumer;
        private IBusConsumer<IPatientVisitUnregisteredMessage> patientVisitUnregisteredConsumer;

        public ConsumersWrapper(IBusConsumer<INewPatientRegisteredMessage> newPatientRegisteredConumser,
                                IBusConsumer<IPatientDataChangedMessage> patientDataChangedConsumer,
                                IBusConsumer<IPatientVisitAcceptedMessage> patientVisitAcceptedConsumer,
                                IBusConsumer<IPatientVisitRegisteredMessage> patientVisitRegisteredConsumer,
                                IBusConsumer<IPatientVisitUnregisteredMessage> patientVisitUnregisteredConsumer)
        {
            this.newPatientRegisteredConumser = newPatientRegisteredConumser;
            this.patientDataChangedConsumer = patientDataChangedConsumer;
            this.patientVisitAcceptedConsumer = patientVisitAcceptedConsumer;
            this.patientVisitRegisteredConsumer = patientVisitRegisteredConsumer;
            this.patientVisitUnregisteredConsumer = patientVisitUnregisteredConsumer;
        }

        public void RegisterConsumers(IBusInstance busInstance)
        {
            busInstance.ConnectConsumer(newPatientRegisteredConumser);
            busInstance.ConnectConsumer(patientDataChangedConsumer);
            busInstance.ConnectConsumer(patientVisitAcceptedConsumer);
            busInstance.ConnectConsumer(patientVisitRegisteredConsumer);
            busInstance.ConnectConsumer(patientVisitUnregisteredConsumer);
        }
    }
}

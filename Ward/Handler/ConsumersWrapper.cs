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
        /*private IBusConsumer<INewPatientRegisteredMessage> newPatientRegisteredConumser;
        private IBusConsumer<IPatientDataChangedMessage> patientDataChangedConsumer;
        private IBusConsumer<IPatientVisitAcceptedMessage> patientVisitAcceptedConsumer;
        private IBusConsumer<IPatientVisitUnregisteredMessage> patientVisitUnregisteredConsumer;

        public ConsumersWrapper(IBusConsumer<INewPatientRegisteredMessage> newPatientRegisteredConumser,
                                IBusConsumer<IPatientDataChangedMessage> patientDataChangedConsumer,
                                IBusConsumer<IPatientVisitAcceptedMessage> patientVisitAcceptedConsumer,
                                IBusConsumer<IPatientVisitUnregisteredMessage> patientVisitUnregisteredConsumer)
        {
            this.newPatientRegisteredConumser = newPatientRegisteredConumser;
            this.patientDataChangedConsumer = patientDataChangedConsumer;
            this.patientVisitAcceptedConsumer = patientVisitAcceptedConsumer;
            this.patientVisitUnregisteredConsumer = patientVisitUnregisteredConsumer;
        }*/

        public void RegisterConsumers(IBusInstance busInstance)
        {
            /*busInstance.ConnectConsumer(newPatientRegisteredConumser);
            busInstance.ConnectConsumer(patientDataChangedConsumer);
            busInstance.ConnectConsumer(patientVisitAcceptedConsumer);
            busInstance.ConnectConsumer(patientVisitUnregisteredConsumer);*/
        }
    }
}

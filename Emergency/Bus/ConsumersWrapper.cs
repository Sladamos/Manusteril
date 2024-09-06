using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Bus
{
    internal class ConsumersWrapper
    {
        /*private IBusConsumer<IPatientVisitRegistrationAcceptedResponse> patientVisitRegistrationAcceptedConsumer;

        private IBusConsumer<IPatientVisitRegistrationDeclinedResponse> patientVisitRegistrationDeclinedConsumer;

        private IBusConsumer<IPatientVisitArrivedMessage> patientVisitArrivedConsumer;

        private IBusConsumer<IPatientAllowedToLeaveMessage> patientAllowedToLeaveConsumer;

        private IBusConsumer<IPatientWardRoomChangedMessage> patientWardRoomChangedConsumer;*/

        public ConsumersWrapper(/*IBusConsumer<IPatientVisitRegistrationAcceptedResponse> patientVisitRegistrationAcceptedConsumer,
                                IBusConsumer<IPatientVisitRegistrationDeclinedResponse> patientVisitRegistrationDeclinedConsumer,
                                IBusConsumer<IPatientVisitArrivedMessage> patientVisitArrivedConsumer,
                                IBusConsumer<IPatientAllowedToLeaveMessage> patientAllowedToLeaveConsumer,
                                IBusConsumer<IPatientWardRoomChangedMessage> patientWardRoomChangedConsumer*/)
        {
            /*this.patientVisitRegistrationAcceptedConsumer = patientVisitRegistrationAcceptedConsumer;
            this.patientVisitRegistrationDeclinedConsumer = patientVisitRegistrationDeclinedConsumer;
            this.patientVisitArrivedConsumer = patientVisitArrivedConsumer;
            this.patientAllowedToLeaveConsumer = patientAllowedToLeaveConsumer;
            this.patientWardRoomChangedConsumer = patientWardRoomChangedConsumer;*/
        }

        public void RegisterConsumers(IBusInstance busInstance)
        {
            /*busInstance.ConnectConsumer(patientVisitRegistrationAcceptedConsumer);
            busInstance.ConnectConsumer(patientVisitRegistrationDeclinedConsumer);
            busInstance.ConnectConsumer(patientVisitArrivedConsumer);
            busInstance.ConnectConsumer(patientAllowedToLeaveConsumer);
            busInstance.ConnectConsumer(patientWardRoomChangedConsumer);*/
        }
    }
}

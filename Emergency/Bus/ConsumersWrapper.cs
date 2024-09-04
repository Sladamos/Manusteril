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
        private IBusConsumer<IPatientAllowedToLeave> patientAllowedToLeaveConsumer;
        private IBusConsumer<IPatientWardChanged> patientWardChangedConsumer;

        public ConsumersWrapper(IBusConsumer<IPatientAllowedToLeave> patientAllowedToLeaveConsumer,
            IBusConsumer<IPatientWardChanged> patientWardChangedConsumer)
        {
            this.patientAllowedToLeaveConsumer = patientAllowedToLeaveConsumer;
            this.patientWardChangedConsumer = patientWardChangedConsumer;
        }

        public void RegisterConsumers(IBusInstance busInstance)
        {
            busInstance.ConnectConsumer(patientAllowedToLeaveConsumer);
            busInstance.ConnectConsumer(patientWardChangedConsumer);
        }
    }
}

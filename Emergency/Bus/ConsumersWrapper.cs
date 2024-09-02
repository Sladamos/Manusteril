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

        public ConsumersWrapper(IBusConsumer<IPatientAllowedToLeave> patientAllowedToLeaveConsumer)
        {
            this.patientAllowedToLeaveConsumer = patientAllowedToLeaveConsumer;
        }

        public void RegisterConsumers(IBusInstance busInstance)
        {
            busInstance.ConnectConsumer(patientAllowedToLeaveConsumer);
        }
    }
}

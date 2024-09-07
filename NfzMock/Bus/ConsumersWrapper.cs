using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Bus
{
    internal class ConsumersWrapper
    {
        private IBusConsumer<IIsPatientInsuredMessage> isPatientInsuredConsumer;

        public ConsumersWrapper(IBusConsumer<IIsPatientInsuredMessage> isPatientInsuredConsumer)
        {
            this.isPatientInsuredConsumer = isPatientInsuredConsumer;
        }

        public void RegisterConsumers(IBusInstance busInstance)
        {
            busInstance.ConnectConsumer(isPatientInsuredConsumer);
        }
    }
}

using log4net;
using MassTransit;
using Messages;
using NfzMock.Bus;
using NfzMock.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Patient
{
    internal class IsPatientInsuredHandler : IBusConsumer<IIsPatientInsuredMessage>
    {
        private ILog logger;

        private MockConfig config;

        public IsPatientInsuredHandler(ILog logger, MockConfig config)
        {
            this.logger = logger;
            this.config = config;
        }

        public string QueueName => "nfzMock_isInsured";

        public async Task Consume(ConsumeContext<IIsPatientInsuredMessage> context)
        {
            logger.Info($"Otrzymano zapytanie o ubezpieczenie: {context.Message}");
            bool isInsured = new Random().Next(100) < config.IsInsuredProbability;
            await context.RespondAsync(new IsPatientInsuredResponse { IsInsured = isInsured,
                PatientPesel = context.Message.PatientPesel });
        }
    }
}

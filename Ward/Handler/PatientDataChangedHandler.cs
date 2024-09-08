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
using Ward.Patient;

namespace Ward.Handler
{
    internal class PatientDataChangedHandler : IBusConsumer<IPatientDataChangedMessage>
    {
        private ILog logger;

        private IPatientService patientService;

        private string wardIdentifier;

        public PatientDataChangedHandler(ILog logger, IPatientService patientService, WardConfig wardConfig)
        {
            this.logger = logger;
            this.patientService = patientService;
            wardIdentifier = wardConfig.WardIdentifier;
        }

        public string QueueName => $"{wardIdentifier}_patientDataChanged";

        public async Task Consume(ConsumeContext<IPatientDataChangedMessage> context)
        {
            try
            {
                var message = context.Message;
                logger.Info($"Otrzymano edytowane dane pacjenta: {message}");
                PatientEntity patient = new()
                {
                    Id = message.PatientId,
                    Pesel = message.PatientPesel,
                    FirstName = message.PatientFirstName,
                    LastName = message.PatientLastName,
                    Address = message.PatientAddress,
                    City = message.PatientCity,
                    PhoneNumber = message.PatientPhoneNumber,
                };
                patientService.EditPatient(patient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze edytowanych danych pacjenta: {ex.Message}");
                throw;
            }
        }
    }
}

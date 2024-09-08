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
using Ward.Visit;

namespace Ward.Handler
{
    internal class NewPatientRegisteredHandler : IBusConsumer<INewPatientRegisteredMessage>
    {
        private ILog logger;

        private IPatientService patientService;

        private string wardIdentifier;

        public NewPatientRegisteredHandler(ILog logger, IPatientService  patientService, WardConfig wardConfig)
        {
            this.logger = logger;
            this.patientService = patientService;
            wardIdentifier = wardConfig.WardIdentifier;
        }

        public string QueueName => $"{wardIdentifier}_newPatient";

        public async Task Consume(ConsumeContext<INewPatientRegisteredMessage> context)
        {
            try
            {
                var message = context.Message;
                logger.Info($"Otrzymano dane nowego pacjenta: {message}");
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
                patientService.AddPatient(patient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze nowego pacjenta: {ex.Message}");
            }
        }
    }
}

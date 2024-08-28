using Emergency.Bus;
using Emergency.Messages;
using Emergency.Validator;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Patient
{
    internal class PatientService : IPatientService
    {
        private readonly IValidatorService validator;

        private readonly IBusInstance busInstance;

        private readonly ILog logger;

        public PatientService(IBusOperator busOperator, ILog logger, IValidatorService validator) {
            this.validator = validator;
            this.logger = logger;
            busInstance = busOperator.CreateBusInstance();
        }

        public void AddPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void DeletePatientByPesel(string pesel)
        {
            var validationResult = validator.validatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }

            var temporaryPatient = new Patient { Pesel = pesel, PatientId = Guid.NewGuid() };
            PatientUnregisteredMessage message = new() { patientId = temporaryPatient.PatientId };
            logger.Info($"Wysyłanie wiadomości o wypisaniu pacjenta: {message}");
            busInstance.Publish(message);
        }

        public Patient GetPatientByPesel(string pesel)
        {
            var validationResult = validator.validatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }
            return new Patient { Pesel = pesel, PatientId = Guid.NewGuid() };
        }
    }
}

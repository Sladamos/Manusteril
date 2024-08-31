using Emergency.Bus;
using Emergency.Messages;
using Emergency.Validator;
using log4net;
using log4net.Core;
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

        private readonly IPatientRepository patientRepository;

        private readonly ILog logger;

        public PatientService(IValidatorService validator,
            IPatientRepository patientRepository,
            ILog logger) {
            this.validator = validator;
            this.patientRepository = patientRepository;
            this.logger = logger;
        }

        public void AddPatient(PatientEntity patient)
        {
            var validationResult = validator.validatePesel(patient.Pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }
            logger.Info($"Dodanie pacjenta: {patient}");
            patientRepository.Save(patient);
        }

        public void EditPatient(PatientEntity patient)
        {
            var validationResult = validator.validatePesel(patient.Pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }
            logger.Info($"Zmiana danych pacjenta, nowe: {patient}");
            patientRepository.Save(patient);
        }

        public PatientEntity GetPatientByPesel(string pesel)
        {
            var validationResult = validator.validatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }
            return patientRepository.GetPatientByPesel(pesel);
        }
    }
}

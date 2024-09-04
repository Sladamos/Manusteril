using Emergency.Bus;
using Emergency.Messages;
using Emergency.Validator;
using log4net;
using log4net.Core;
using Messages;
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

        private readonly IPatientEventRepository eventRepository;

        private readonly ILog logger;

        public PatientService(IValidatorService validator,
            IPatientRepository patientRepository,
            IPatientEventRepository eventRepository,
            ILog logger) {
            this.validator = validator;
            this.patientRepository = patientRepository;
            this.eventRepository = eventRepository;
            this.logger = logger;
        }

        public void AddPatient(PatientEntity patient)
        {
            ValiateStandardPatientFields(patient);
            ValidatePesel(patient.Pesel);
            ValidatePhoneNumber(patient.PhoneNumber);
            logger.Info($"Dodanie pacjenta: {patient}");
            patientRepository.Save(patient);
            eventRepository.Create(patient);
        }

        public void EditPatient(PatientEntity patient)
        {
            ValiateStandardPatientFields(patient);
            ValidatePesel(patient.Pesel);
            ValidatePhoneNumber(patient.PhoneNumber);
            logger.Info($"Zmiana danych pacjenta, nowe: {patient}");
            patientRepository.Save(patient);
            eventRepository.Update(patient);
        }

        public PatientEntity GetPatientByPesel(string pesel)
        {
            ValidatePesel(pesel);
            return patientRepository.GetPatientByPesel(pesel);
        }

        public async Task<IIsPatientInsuredResponse> IsPatientInsured(string pesel)
        {
            ValidatePesel(pesel);
            return await eventRepository.IsPatientInsured(pesel);
        }

        private void ValidatePesel(string pesel)
        {
            var validationResult = validator.ValidatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException(validationResult.ValidatorMessage);
            }
        }

        private void ValidatePhoneNumber(string phoneNumber)
        {
            var validationResult = validator.ValidatePhoneNumber(phoneNumber);
            if (!validationResult.IsValid)
            {
                throw new InvalidPhoneNumberException(validationResult.ValidatorMessage);
            }
        }

        private void ValiateStandardPatientFields(PatientEntity patient)
        {
            var fieldNames = new Dictionary<string, string>
            {
                { "Imie", patient.FirstName },
                { "Nazwisko", patient.LastName },
                { "Miasto", patient.City },
                { "Adres", patient.Address }
            };
            var missingField = fieldNames.FirstOrDefault(kv => string.IsNullOrEmpty(kv.Value));

            if (missingField.Key != null)
            {
                throw new InvalidPatientDataException($"Nie podano pola {missingField.Key}");
            }
        }
    }
}

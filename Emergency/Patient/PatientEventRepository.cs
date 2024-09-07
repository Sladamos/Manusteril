using Emergency.Bus;
using Emergency.Message;
using Emergency.Messages;
using Emergency.Validator;
using log4net;
using log4net.Core;
using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Patient
{
    internal class PatientEventRepository : IPatientEventRepository
    {
        private IBusInstance busInstance;

        private IBusClient<IIsPatientInsuredMessage> busClient;

        private ILog logger;

        public PatientEventRepository(IBusOperator busOperator,
            ILog logger)
        {
            busInstance = busOperator.CreateBusInstance();
            busClient = busOperator.CreateBusClient<IIsPatientInsuredMessage>();
            this.logger = logger;
        }

        public void Create(PatientEntity patient)
        {
            NewPatientRegisteredMessage message = new()
            {
                PatientId = patient.Id,
                PatientPesel = patient.Pesel,
                PatientFirstName = patient.FirstName,
                PatientLastName = patient.LastName,
                PatientCity = patient.City,
                PatientAddress = patient.Address,
                PatientPhoneNumber = patient.PhoneNumber
            };
            logger.Info($"Wysłanie wiadomości o utworzeniu pacjenta: {patient}");
            busInstance.Publish(message);
        }

        public async Task<IIsPatientInsuredResponse> IsPatientInsured(string patientPesel)
        {
            IsPatientInsuredMessage message = new() { PatientPesel = patientPesel };
            var response = busClient.GetResponse<IIsPatientInsuredResponse>(message, new TimeSpan(0, 0, 2));
            return await response;
        }

        public void Update(PatientEntity patient)
        {
            PatientDataChangedMessage message = new()
            {
                PatientId = patient.Id,
                PatientPesel = patient.Pesel,
                PatientFirstName = patient.FirstName,
                PatientLastName = patient.LastName,
                PatientCity = patient.City,
                PatientAddress = patient.Address,
                PatientPhoneNumber = patient.PhoneNumber
            };
            logger.Info($"Wysłanie wiadomości o zmianie danych pacjenta: {patient}");
            busInstance.Publish(message);
        }
    }
}

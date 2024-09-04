using Emergency.Patient;
using Emergency.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.EditPatient
{
    internal class EditPatientLogicCommand : ICommand
    {
        private IPatientService patientService;

        private PatientInfo patientInfo;

        public string Name => "Edytuj";

        public string Description => "Zatwierdź nowe dane pacjenta";

        public Action? OnPatientEdited;

        public EditPatientLogicCommand(IPatientService patientService, PatientInfo patientInfo)
        {
            this.patientService = patientService;
            this.patientInfo = patientInfo;
        }

        public async Task Execute()
        {
            Console.WriteLine("Edytowanie danych pacjenta");
            try
            {
                EditPatient();
            }
            catch (InvalidPeselException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidPhoneNumberException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void EditPatient()
        {
            string pesel = patientInfo.PeselSupplier();
            string firstName = patientInfo.FirstNameSupplier();
            string lastName = patientInfo.LastNameSupplier();
            string city = patientInfo.CitySupplier();
            string phoneNumber = patientInfo.PhoneNumberSupplier();
            string address = patientInfo.AddressSupplier();
            var patient = patientService.GetPatientByPesel(pesel);
            patient.FirstName = firstName;
            patient.LastName = lastName;
            patient.PhoneNumber = phoneNumber;
            patient.Address = address;
            patient.City = city;
            patientService.EditPatient(patient);
            OnPatientEdited?.Invoke();
        }
    }
}

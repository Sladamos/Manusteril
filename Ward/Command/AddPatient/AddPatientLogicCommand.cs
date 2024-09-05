using Ward.Patient;
using Ward.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Command.AddPatient
{
    internal class AddPatientLogicCommand : ICommand
    {
        private IPatientService patientService;

        private PatientInfo patientInfo;

        public string Name => "Nowy";

        public string Description => "Dodaj nowego pacjenta";

        public Action? OnPatientCreated;

        public AddPatientLogicCommand(IPatientService patientService, PatientInfo patientInfo)
        {
            this.patientService = patientService;
            this.patientInfo = patientInfo;
        }

        public async Task Execute()
        {
            Console.WriteLine("Dodawanie pacjenta");
            try
            {
                CreatePatient();
            }
            catch (InvalidPeselException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidPhoneNumberException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidPatientDataException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CreatePatient()
        {
            string firstName = patientInfo.FirstNameSupplier();
            string lastName = patientInfo.LastNameSupplier();
            string city = patientInfo.CitySupplier();
            string phoneNumber = patientInfo.PhoneNumberSupplier();
            string address = patientInfo.AddressSupplier();
            string pesel = patientInfo.PeselSupplier();
            PatientEntity patient = new()
            {
                Id = Guid.NewGuid(),
                Pesel = pesel,
                FirstName = firstName,
                LastName = lastName,
                City = city,
                PhoneNumber = phoneNumber,
                Address = address
            };
            patientService.AddPatient(patient);
            OnPatientCreated?.Invoke();
        }
    }
}

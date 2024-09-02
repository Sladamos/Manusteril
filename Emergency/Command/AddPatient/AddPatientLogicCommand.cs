using Emergency.Patient;
using Emergency.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.AddPatient
{
    internal class AddPatientLogicCommand : ICommand
    {
        private IPatientService patientService;

        private Func<string> peselSupplier;

        private Func<string> firstNameSupplier;

        private Func<string> lastNameSupplier;

        private Func<string> phoneNumberSupplier;

        private Func<string> addressSupplier;

        private Func<string> citySupplier;

        public string Name => "Nowy";

        public string Description => "Dodaj nowego pacjenta";

        public Action? OnPatientCreated;

        public AddPatientLogicCommand(IPatientService patientService,
            Func<string> peselSupplier,
            Func<string> firstNameSupplier,
            Func<string> lastNameSupplier,
            Func<string> phoneNumberSupplier,
            Func<string> addressSupplier,
            Func<string> citySupplier)
        {
            this.patientService = patientService;
            this.peselSupplier = peselSupplier;
            this.firstNameSupplier = firstNameSupplier;
            this.lastNameSupplier = lastNameSupplier;
            this.phoneNumberSupplier = phoneNumberSupplier;
            this.addressSupplier = addressSupplier;
            this.citySupplier = citySupplier;
        }

        public async Task Execute()
        {
            Console.WriteLine("Dodawanie pacjenta");
        }

        private void CreatePatient()
        {
            string firstName = firstNameSupplier();
            string lastName = lastNameSupplier();
            string city = citySupplier();
            string phoneNumber = phoneNumberSupplier();
            string address = addressSupplier();
            string pesel = peselSupplier();
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

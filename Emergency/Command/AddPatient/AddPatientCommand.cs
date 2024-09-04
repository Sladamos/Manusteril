using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;
using Emergency.Validator;
using Messages;

namespace Emergency.Command.AddPatient
{
    internal class AddPatientCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private string pesel = "";

        private string phoneNumber = "";

        private string firstName = "";

        private string lastName = "";

        private string address = "";

        private string city = "";

        public string Name => "Nowy";

        public string Description => "Dodaj nowego pacjenta";

        public AddPatientCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            SelectStringCommand selectPhoneNumberCommand = commandsFactory.SelectStringCommand("Numer telefonu", GetPhoneNumber);
            SelectStringCommand selectFirstNameCommand = commandsFactory.SelectStringCommand("Imie", GetFirstName);
            SelectStringCommand selectLastNameCommand = commandsFactory.SelectStringCommand("Nazwisko", GetLastName);
            SelectStringCommand selectCityCommand = commandsFactory.SelectStringCommand("Miasto", GetCity);
            SelectStringCommand selectAddressCommand = commandsFactory.SelectStringCommand("Adres", GetAddress);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            PatientInfo patientInfo = new()
            {
                AddressSupplier = GetAddress,
                CitySupplier = GetCity,
                FirstNameSupplier = GetFirstName,
                LastNameSupplier = GetLastName,
                PeselSupplier = GetPesel,
                PhoneNumberSupplier = GetPhoneNumber
            };
            AddPatientLogicCommand addPatientLogicCommand = commandsFactory.AddPatientLogicCommand(patientInfo);
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[selectAddressCommand.Name] = selectAddressCommand;
            commands[selectCityCommand.Name] = selectCityCommand;
            commands[selectFirstNameCommand.Name] = selectFirstNameCommand;
            commands[selectLastNameCommand.Name] = selectLastNameCommand;
            commands[selectPhoneNumberCommand.Name] = selectPhoneNumberCommand;
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[addPatientLogicCommand.Name] = addPatientLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            selectPhoneNumberCommand.OnStringSelected += OnPhoneNumberSelected;
            selectCityCommand.OnStringSelected += OnCitySelected;
            selectAddressCommand.OnStringSelected += OnAddressSelected;
            selectFirstNameCommand.OnStringSelected += OnFirstNameSelected;
            selectLastNameCommand.OnStringSelected += OnLastNameSelected;

            addPatientLogicCommand.OnPatientCreated += OnPatientCreated;
        }

        public async Task Execute()
        {
            Console.WriteLine("Rejestrowanie pacjenta");
            enabled = true;
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
            pesel = "";
            city = "";
            address = "";
            phoneNumber = "";
            firstName = "";
            lastName = "";
        }

        private void OnPeselSelected(string pesel)
        {
            var validationResult = validator.ValidatePesel(pesel);
            if (validationResult.IsValid)
            {
                this.pesel = pesel;
            }
            else
            {
                Console.WriteLine(validationResult?.ValidatorMessage ?? "Niepoprawny PESEL");
            }
        }

        private void OnPhoneNumberSelected(string phoneNumber)
        {
            var validationResult = validator.ValidatePhoneNumber(phoneNumber);
            if (validationResult.IsValid)
            {
                this.phoneNumber = phoneNumber;
            }
            else
            {
                Console.WriteLine(validationResult?.ValidatorMessage ?? "Niepoprawny numer telefonu");
            }
        }

        private void OnCitySelected(string city)
        {
            this.city = city;
        }

        private void OnFirstNameSelected(string firstName)
        {
            this.firstName = firstName;
        }

        private void OnLastNameSelected(string lastName)
        {
            this.lastName = lastName;
        }

        private void OnAddressSelected(string address)
        {
            this.address = address;
        }

        private void OnPatientCreated()
        {
            Console.WriteLine("Pomyślnie dodano pacjenta");
            enabled = false;
        }

        private string GetPesel() { return pesel; }

        private string GetFirstName() { return firstName; }

        private string GetLastName() { return lastName; }

        private string GetPhoneNumber() { return phoneNumber; }

        private string GetAddress() { return address; }

        private string GetCity() { return city; }
    }
}

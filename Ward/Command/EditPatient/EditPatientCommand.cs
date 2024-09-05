using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using Ward.Patient;
using Ward.Validator;
using Messages;

namespace Ward.Command.EditPatient
{
    internal class EditPatientCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private readonly IPatientService patientService;

        private string pesel = "";

        private string phoneNumber = "";

        private string firstName = "";

        private string lastName = "";

        private string address = "";

        private string city = "";

        public string Name => "Edytuj";

        public string Description => "Zmień dane pacjenta";

        public EditPatientCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator,
            IPatientService patientService)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            this.patientService = patientService;
            Condition condition = new () { Predicate = () => !string.IsNullOrEmpty(pesel), Reason = "Najpierw należy wybrać PESEL" };
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
            EditPatientLogicCommand editPatientLogicCommand = commandsFactory.EditPatientLogicCommand(patientInfo);
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[selectPeselCommand.Name] = new ConditionalCommand(selectPeselCommand, condition);
            commands[selectAddressCommand.Name] = new ConditionalCommand(selectAddressCommand, condition);
            commands[selectCityCommand.Name] = new ConditionalCommand(selectCityCommand, condition);
            commands[selectFirstNameCommand.Name] = new ConditionalCommand(selectFirstNameCommand, condition);
            commands[selectLastNameCommand.Name] = new ConditionalCommand(selectLastNameCommand, condition);
            commands[selectPhoneNumberCommand.Name] = new ConditionalCommand(selectPhoneNumberCommand, condition);
            commands[editPatientLogicCommand.Name] = editPatientLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            selectPhoneNumberCommand.OnStringSelected += OnPhoneNumberSelected;
            selectCityCommand.OnStringSelected += OnCitySelected;
            selectAddressCommand.OnStringSelected += OnAddressSelected;
            selectFirstNameCommand.OnStringSelected += OnFirstNameSelected;
            selectLastNameCommand.OnStringSelected += OnLastNameSelected;

            editPatientLogicCommand.OnPatientEdited += OnPatientEdited;
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
                try
                {
                    var patient = patientService.GetPatientByPesel(pesel);
                    this.pesel = pesel;
                    this.firstName = patient.FirstName;
                    this.lastName = patient.LastName;
                    this.phoneNumber = patient.PhoneNumber;
                    this.city = patient.City;
                    this.address = patient.Address;
                }
                catch (UnknownPatientException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidPeselException ex)
                {
                    Console.WriteLine(ex.Message);
                }
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

        private void OnPatientEdited()
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

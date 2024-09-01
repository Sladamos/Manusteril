using Emergency.Command.DeletePatient;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;
using Emergency.Validator;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.CheckInsurance
{
    internal class CheckInsuranceCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private string pesel = "";

        public CheckInsuranceCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            CheckInsuranceLogicCommand checkInsuranceLogicCommand = commandsFactory.CheckInsuranceLogicCommand(GetPesel);
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[checkInsuranceLogicCommand.Name] = checkInsuranceLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            checkInsuranceLogicCommand.OnResponseArrived += OnInsuranceChecked;
        }

        public string Name => "Ubezpieczenie";

        public string Description => "Sprawdź, czy pacjent jest ubezpieczony";

        public async Task Execute()
        {
            Console.WriteLine("Sprawdzanie ubezpieczenia pacjenta");
            enabled = true;
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
        }

        private void OnPeselSelected(string pesel)
        {
            var validationResult = validator.validatePesel(pesel);
            if (validationResult.IsValid)
            {
                this.pesel = pesel;
            }
            else
            {
                Console.WriteLine(validationResult?.ValidatorMessage ?? "Niepoprawny PESEL");
            }
        }

        private void OnInsuranceChecked(IIsPatientInsuredResponse isPatientInsured)
        {
            if (isPatientInsured.PatientPesel == pesel)
            {
                Console.WriteLine($"Pacjent${(isPatientInsured.IsInsured ? "" : " nie")} jest ubezpieczony");
            }
            else
            {
                Console.WriteLine("Niepoprawna odpowiedź od NFZ - dotyczy innego pacjenta");
            }
            enabled = false;
        }

        private string GetPesel() { return pesel; }
    }
}

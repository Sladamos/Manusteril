using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using Ward.Command.Patients.FindPatientRoom;
using Ward.Room;
using Ward.Validator;

namespace Ward.Command.Patients.AllowPatientToLeave
{
    internal class AllowPatientToLeaveCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private string pesel = "";

        private string pwzNumber = "";

        private bool leavedAtOwnRisk = false;

        public string Name => "Zezwól";

        public string Description => "Wydaj zezwolenie na opuszczenie szpitala przez pacjenta";

        public AllowPatientToLeaveCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            SelectStringCommand selectPwzCommand = commandsFactory.SelectStringCommand("PWZ", GetPwzNumber);
            MultichoiceCommand<bool> leavedAtOwnRiskCommand = commandsFactory.LeavedAtOwnRiskCommand(() => leavedAtOwnRisk);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            AllowPatientToLeaveLogicCommand allowPatientToLeaveLogicCommand = commandsFactory.AllowPatientToLeaveLogicCommand(GetPesel, GetPwzNumber, () => leavedAtOwnRisk);
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[selectPwzCommand.Name] = selectPwzCommand;
            commands[leavedAtOwnRiskCommand.Name] = leavedAtOwnRiskCommand;
            commands[allowPatientToLeaveLogicCommand.Name] = allowPatientToLeaveLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            selectPwzCommand.OnStringSelected += OnPwzNumberSelected;
            leavedAtOwnRiskCommand.OnValueSelected += OnOwnRiskSelected;
            allowPatientToLeaveLogicCommand.OnAllowedToLeave += OnAllowedToLeave;
        }

        public async Task Execute()
        {
            Console.WriteLine("Wydanie wypiski pacjentowi");
            enabled = true;
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
            pesel = "";
            pwzNumber = "";
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

        private void OnPwzNumberSelected(string pwz)
        {
            var validationResult = validator.ValidatePwz(pwz);
            if (validationResult.IsValid)
            {
                this.pwzNumber = pwz;
            }
            else
            {
                Console.WriteLine(validationResult?.ValidatorMessage ?? "Niepoprawny numer PWZ");
            }
        }

        private void OnOwnRiskSelected(bool leavedAtOwnRisk)
        {
            this.leavedAtOwnRisk = leavedAtOwnRisk;
        }



        private void OnAllowedToLeave()
        {
            enabled = false;
        }

        private string GetPesel() { return pesel; }

        private string GetPwzNumber() { return pwzNumber; }
    }
}

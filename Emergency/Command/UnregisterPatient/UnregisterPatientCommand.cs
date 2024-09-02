using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;
using Emergency.Validator;

namespace Emergency.Command.UnregisterPatient
{
    internal class UnregisterPatientCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private string pesel = "";

        public string Name => "Wypisz";

        public string Description => "Wypisz pacjenta";

        public UnregisterPatientCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            UnregisterPatientLogicCommand unregisterPatientLogicCommand = commandsFactory.UnregisterPatientLogicCommand(GetPesel);
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[unregisterPatientLogicCommand.Name] = unregisterPatientLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            unregisterPatientLogicCommand.OnPatientDeleted += OnPatientDeleted;
        }

        public async Task Execute()
        {
            Console.WriteLine("Wypisywanie pacjenta");
            enabled = true;
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
            pesel = "";
        }

        private void OnPeselSelected(string pesel)
        {
            var validationResult = validator.validatePesel(pesel);
            if (validationResult.IsValid)
            {
                this.pesel = pesel;
            } else
            {
                Console.WriteLine(validationResult?.ValidatorMessage ?? "Niepoprawny PESEL");
            }
        }

        private void OnPatientDeleted()
        {
            Console.WriteLine("Pomyślnie wypisano pacjenta");
            enabled = false;
        }

        private string GetPesel() { return pesel; }
    }
}

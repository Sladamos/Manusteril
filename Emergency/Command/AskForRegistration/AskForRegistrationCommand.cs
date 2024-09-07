using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;
using Emergency.Validator;
using Messages;

namespace Emergency.Command.AskForRegistration
{
    internal class AskForRegistrationCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private string pesel = "";

        private WardType ward = WardType.NONE;

        public string Name => "Zapytaj";

        public string Description => "Zapytaj czy ktoś przyjmie pacjenta";

        public AskForRegistrationCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            MultichoiceCommand<WardType> selectWardCommand = commandsFactory.SelectWardCommand(GetWard);
            AskForRegistrationLogicCommand askForRegistrationLogicCommand= commandsFactory.AskForRegistrationLogicCommand(GetPesel, GetWard);
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[selectWardCommand.Name] = selectWardCommand;
            commands[askForRegistrationLogicCommand.Name] = askForRegistrationLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            selectWardCommand.OnValueSelected += OnWardSelected;
            askForRegistrationLogicCommand.OnQuestionSent += OnQuestionSent;
        }

        public async Task Execute()
        {
            Console.WriteLine("Zapytanie o rejestrację pacjenta");
            enabled = true;
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
            pesel = "";
            ward = WardType.NONE;
        }

        private void OnPeselSelected(string pesel)
        {
            var validationResult = validator.ValidatePesel(pesel);
            if (validationResult.IsValid)
            {
                this.pesel = pesel;
            } else
            {
                Console.WriteLine(validationResult?.ValidatorMessage ?? "Niepoprawny PESEL");
            }
        }

        private void OnWardSelected(WardType ward)
        {
            this.ward = ward;
        }

        private void OnQuestionSent()
        {
            Console.WriteLine("Pomyślnie wysłano zapytanie");
            enabled = false;
        }

        private string GetPesel() { return pesel; }

        private WardType GetWard() { return ward; }
    }
}

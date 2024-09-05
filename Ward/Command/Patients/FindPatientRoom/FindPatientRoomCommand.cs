using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using Ward.Room;
using Ward.Validator;

namespace Ward.Command.Patients.FindPatientRoom
{
    internal class FindPatientRoomCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private string pesel = "";

        public string Name => "Znajdź";

        public string Description => "Znajdź salę na której leży pacjent";

        public FindPatientRoomCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            FindPatientRoomLogicCommand findPatientRoomLogicCommand = commandsFactory.FindPatientRoomLogicCommand(GetPesel);
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[findPatientRoomLogicCommand.Name] = findPatientRoomLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            findPatientRoomLogicCommand.OnRoomFound += OnRoomFound;
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

        private void OnRoomFound(RoomEntity entity)
        {
            Console.WriteLine($"Pacjent leży na sali {entity.Number}");
            enabled = false;
        }

        private string GetPesel() { return pesel; }
    }
}

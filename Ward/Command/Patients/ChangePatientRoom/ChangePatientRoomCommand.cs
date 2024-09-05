using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using Ward.Command.Patients.ChangePatientWard;
using Ward.Room;
using Ward.Validator;

namespace Ward.Command.Patients.ChangePatientRoom
{
    internal class ChangePatientRoomCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private IRoomService roomService;

        private string pesel = "";

        private string roomNumber = "";

        public string Name => "Przenieś";

        public string Description => "Przenieś pacjenta do innej sali";

        public ChangePatientRoomCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IRoomService roomService,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.roomService = roomService;
            this.validator = validator;
            Multichoice<string> selectRoomMultichoice = new()
            {
                Values = this.roomService.GetAll().Where(room => room.OccupiedBeds < room.Capacity).Select(room => room.Number).ToList(),
                DefaultDescription = "Wybierz salę",
                Name = "Sala",
                ParameterSupplier = GetRoomNumber,
                ParameterValidator = (room) => !string.IsNullOrEmpty(room)
            };
            MultichoiceCommand<string> selectRoomCommand = commandsFactory.SelectRoomCommand(selectRoomMultichoice);
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            ChangePatientRoomLogicCommand changePatientRoomLogicCommand = commandsFactory.ChangePatientRoomLogicCommand(GetPesel, GetRoomNumber);
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[selectRoomCommand.Name] = selectRoomCommand;
            commands[changePatientRoomLogicCommand.Name] = changePatientRoomLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            selectRoomCommand.OnValueSelected += OnRoomNumberSelected;
            changePatientRoomLogicCommand.OnPatientTransfered = OnRoomChanged;
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

        private void OnRoomNumberSelected(string roomNumber)
        {
            this.roomNumber = roomNumber;
        }

        private void OnRoomChanged()
        {
            Console.WriteLine($"Pomyślnie zmieniono salę");
            enabled = false;
        }

        private string GetPesel() { return pesel; }

        private string GetRoomNumber() { return roomNumber; }
    }
}

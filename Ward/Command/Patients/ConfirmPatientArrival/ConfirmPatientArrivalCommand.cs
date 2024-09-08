using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Command.DisplayRoomOccupation;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using Ward.Command.Patients.FindPatientRoom;
using Ward.Room;
using Ward.Validator;

namespace Ward.Command.Patients.ConfirmPatientArrival
{
    internal class ConfirmPatientArrivalCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private string pesel = "";

        private string roomNumber = "";

        public string Name => "Potwierdź";

        public string Description => "Potwierdź przybycie pacjenta";

        public ConfirmPatientArrivalCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            SelectStringCommand selectRoomNumber = commandsFactory.SelectStringCommand("Numer sali", GetRoomNumber);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            DisplayFreeRoomsCommand displayFreeRoomsCommand = commandsFactory.DisplayFreeRoomsCommand();
            ConfirmPatientArrivalLogicCommand confirmPatientArrivalLogicCommand = commandsFactory.ConfirmPatientArrivalLogicCommand(GetPesel, GetRoomNumber);
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[selectRoomNumber.Name] = selectRoomNumber;
            commands[displayFreeRoomsCommand.Name] = displayFreeRoomsCommand;
            commands[confirmPatientArrivalLogicCommand.Name] = confirmPatientArrivalLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            selectRoomNumber.OnStringSelected += OnRoomNumberSelected;
            confirmPatientArrivalLogicCommand.OnConfirmed += OnConfirmed;
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

        private void OnConfirmed()
        {
            Console.WriteLine("Potwierdzono przybycie pacjenta");
            enabled = false;
        }

        private string GetPesel() { return pesel; }

        private string GetRoomNumber() { return roomNumber; }
    }
}

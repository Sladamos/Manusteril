﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Bus;
using Ward.Command.DisplayRoomOccupation;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using Ward.Command;
using Ward.Command.Patients.FindPatientRoom;
using Ward.Command.Patients.ChangePatientRoom;
using Ward.Command.Patients.AllowPatientToLeave;
using Ward.Command.Patients.ConfirmPatientArrival;

namespace Ward.Command.Patients
{
    internal class PatientCommands : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        public string Name => "Pacjent";

        public string Description => "Komendy powiązane z pacjentem";

        public PatientCommands(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner)
        {
            this.commandsExecutioner = commandsExecutioner;
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            FindPatientRoomCommand findPatientRoomCommand = commandsFactory.FindPatientRoomCommand();
            ChangePatientRoomCommand changePatientRoomCommand = commandsFactory.ChangePatientRoomCommand();
            AllowPatientToLeaveCommand allowPatientToLeaveCommand = commandsFactory.AllowPatientToLeaveCommand();
            ConfirmPatientArrivalCommand confirmPatientArrivalCommand = commandsFactory.ConfirmPatientArrivalCommand();
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[findPatientRoomCommand.Name] = findPatientRoomCommand;
            commands[changePatientRoomCommand.Name] = changePatientRoomCommand;
            commands[allowPatientToLeaveCommand.Name] = allowPatientToLeaveCommand;
            commands[confirmPatientArrivalCommand.Name] = confirmPatientArrivalCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
        }

        public async Task Execute()
        {
            enabled = true;
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
        }
    }
}

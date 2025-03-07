﻿using Ward.Bus;
using Ward.Command;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Command.DisplayRoomOccupation;
using Ward.Command.Patients;
using Ward.Handler;
using Ward.Command.Patients.ConsidierPatientAcceptance;

namespace Ward
{
    internal class Menu : IMenu
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IBusInstance busInstance;

        private readonly ConsumersWrapper consumers;
        
        public Menu(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IBusOperator busOperator,
            ConsumersWrapper consumers)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.busInstance = busOperator.CreateBusInstance();
            this.consumers = consumers;
            ExitProgramCommand exitProgramCommand = commandsFactory.ExitProgramCommand();
            DisplayRoomOccupationCommand displayRoomOccupationCommand = commandsFactory.DisplayRoomOccupationCommand();
            DisplayFreeRoomsCommand displayFreeRoomsCommand = commandsFactory.DisplayFreeRoomsCommand();
            ConsiderPatientAcceptanceCommand considerPatientAcceptanceCommand = commandsFactory.ConsiderPatientAcceptanceCommand();
            PatientCommands patientCommands = commandsFactory.CreatePatientsCommands();
            commands[exitProgramCommand.Name] = exitProgramCommand;
            commands[displayRoomOccupationCommand.Name] = displayRoomOccupationCommand;
            commands[displayFreeRoomsCommand.Name] = displayFreeRoomsCommand;
            commands[considerPatientAcceptanceCommand.Name] = considerPatientAcceptanceCommand;
            commands[patientCommands.Name] = patientCommands;
            exitProgramCommand.ProgramExited += () => enabled = false;
        }

        public async Task Start()
        {
            await busInstance.Start();
            enabled = true;
            consumers.RegisterConsumers(busInstance);
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
            await busInstance.Stop();
        }
    }
}

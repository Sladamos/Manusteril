﻿using Emergency.Bus;
using Emergency.Command;
using Emergency.Command.DeletePatient;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency
{
    internal class Menu : IMenu
    {

        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IBusInstance busInstance;

        public Menu(ICommandsFactory commandsFactory, ICommandsExecutioner commandsExecutioner, IBusOperator busOperator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.busInstance = busOperator.CreateBusInstance();
            ExitProgramCommand exitProgramCommand = commandsFactory.ExitProgramCommand();
            CheckInsuranceCommand checkInsuranceCommand = commandsFactory.CheckInsuranceCommand();
            DeletePatientCommand deletePatientCommand = commandsFactory.DeletePatientCommand();
            AddPatientCommand addPatientCommand = commandsFactory.AddPatientCommand();
            commands[exitProgramCommand.Name] = exitProgramCommand;
            commands[checkInsuranceCommand.Name] = checkInsuranceCommand;
            commands[deletePatientCommand.Name] = deletePatientCommand;
            commands[addPatientCommand.Name] = addPatientCommand;
            exitProgramCommand.ProgramExited += () => enabled = false;
        }

        public void Start()
        {
            busInstance.Start();
            enabled = true;
            while (enabled)
            {
                commandsExecutioner.Execute(commands);
            }
            busInstance.Stop();
        }
    }
}

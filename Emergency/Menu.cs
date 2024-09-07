using Emergency.Bus;
using Emergency.Command;
using Emergency.Command.CheckInsurance;
using Emergency.Command.UnregisterPatient;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.RegisterPatient;
using Emergency.Command.AddPatient;
using Emergency.Command.EditPatient;
using Emergency.Handler;
using Emergency.Command.AskForRegistration;

namespace Emergency
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
            CheckInsuranceCommand checkInsuranceCommand = commandsFactory.CheckInsuranceCommand();
            AskForRegistrationCommand askForRegistrationCommand = commandsFactory.AskForRegistrationCommand();
            RegisterPatientCommand registerPatientCommand = commandsFactory.RegisterPatientCommand();
            UnregisterPatientCommand unregisterPatientCommand = commandsFactory.UnregisterPatientCommand();
            AddPatientCommand addPatientCommand = commandsFactory.AddPatientCommand();
            EditPatientCommand editPatientCommand = commandsFactory.EditPatientCommand();
            commands[exitProgramCommand.Name] = exitProgramCommand;
            commands[checkInsuranceCommand.Name] = checkInsuranceCommand;
            commands[askForRegistrationCommand.Name] = askForRegistrationCommand;
            commands[registerPatientCommand.Name] = registerPatientCommand;
            commands[unregisterPatientCommand.Name] = unregisterPatientCommand;
            commands[addPatientCommand.Name] = addPatientCommand;
            commands[editPatientCommand.Name] = editPatientCommand;
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

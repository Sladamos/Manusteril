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
            UnregisterPatientCommand deletePatientCommand = commandsFactory.UnregisterPatientCommand();
            RegisterPatientCommand registerPatientCommand = commandsFactory.RegisterPatientCommand();
            AddPatientCommand addPatientCommand = commandsFactory.AddPatientCommand();
            commands[exitProgramCommand.Name] = exitProgramCommand;
            commands[checkInsuranceCommand.Name] = checkInsuranceCommand;
            commands[deletePatientCommand.Name] = deletePatientCommand;
            commands[addPatientCommand.Name] = addPatientCommand;
            commands[registerPatientCommand.Name] = registerPatientCommand;
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

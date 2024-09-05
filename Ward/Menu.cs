using Ward.Bus;
using Ward.Command;
using Ward.Command.CheckInsurance;
using Ward.Command.UnregisterPatient;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Command.RegisterPatient;
using Ward.Command.AddPatient;
using Ward.Command.EditPatient;

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
            CheckInsuranceCommand checkInsuranceCommand = commandsFactory.CheckInsuranceCommand();
            UnregisterPatientCommand deletePatientCommand = commandsFactory.UnregisterPatientCommand();
            RegisterPatientCommand registerPatientCommand = commandsFactory.RegisterPatientCommand();
            AddPatientCommand addPatientCommand = commandsFactory.AddPatientCommand();
            EditPatientCommand editPatientCommand = commandsFactory.EditPatientCommand();
            commands[exitProgramCommand.Name] = exitProgramCommand;
            commands[checkInsuranceCommand.Name] = checkInsuranceCommand;
            commands[deletePatientCommand.Name] = deletePatientCommand;
            commands[addPatientCommand.Name] = addPatientCommand;
            commands[editPatientCommand.Name] = editPatientCommand;
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

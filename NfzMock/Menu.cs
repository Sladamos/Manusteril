using NfzMock.Bus;
using NfzMock.Command;
using NfzMock.Command.Executioner;
using NfzMock.Command.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock
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
            commands[exitProgramCommand.Name] = exitProgramCommand;
            exitProgramCommand.ProgramExited += () => enabled = false;
        }

        public async Task Start()
        {
            await busInstance.Start();
            enabled = true;
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
            await busInstance.Stop();
        }
    }
}

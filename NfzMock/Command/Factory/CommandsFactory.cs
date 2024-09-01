using NfzMock.Bus;
using NfzMock.Command.Executioner;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Command.Factory
{
    internal class CommandsFactory : ICommandsFactory
    {

        public ExitProgramCommand ExitProgramCommand()
        {
            return new ExitProgramCommand();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.Executioner
{
    internal interface ICommandsExecutioner
    {
        void Execute(Dictionary<string, ICommand> commands);
    }
}

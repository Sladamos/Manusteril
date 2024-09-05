using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Command.Executioner
{
    internal interface ICommandsExecutioner
    {
        Task Execute(Dictionary<string, ICommand> commands);
    }
}

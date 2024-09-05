using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Command
{
    internal class ExitProgramCommand : ICommand
    {
        public string Name => "Wyjscie";

        public string Description => "Wyjście z programu";

        public event Action? ProgramExited;

        public async Task Execute()
        {
            ProgramExited?.Invoke();
        }
    }
}

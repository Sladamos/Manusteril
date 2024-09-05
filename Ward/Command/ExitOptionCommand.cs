using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Command
{
    internal class ExitOptionCommand : ICommand
    {
        public string Name => "Wroc";

        public string Description => "Powrót do poprzedniego menu";

        public event Action? OptionExited;

        public async Task Execute()
        {
            OptionExited?.Invoke();
        }
    }
}

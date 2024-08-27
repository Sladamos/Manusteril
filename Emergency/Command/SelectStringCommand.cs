using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class SelectStringCommand : ICommand
    {
        private readonly string _parameter;

        public SelectStringCommand(string parameter) {
            this._parameter = parameter;
        }

        public string Name => _parameter;

        public string Description => $"Wybierz {Name}";

        public event Action<string>? OnStringSelected;

        public void Execute()
        {
            Console.WriteLine($"Podaj {Name}");
            string input = Console.ReadLine() ?? string.Empty;
            OnStringSelected?.Invoke(input);
        }
    }
}

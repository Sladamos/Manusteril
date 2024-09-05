using Ward.Command.Executioner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Command
{
    internal class SelectStringCommand : ICommand
    {
        private readonly string _parameter;

        private readonly Func<string>? _parameterSupplier;

        public SelectStringCommand(string parameter, Func<string>? parameterSupplier) {
            this._parameter = parameter;
            this._parameterSupplier = parameterSupplier;
        }

        public string Name => _parameter.Capitalize();

        public string Description
        {
            get
            {
                string communicate = $"Podaj {_parameter}";
                if(_parameterSupplier != null)
                {
                    string parameter = _parameterSupplier();
                    if(!string.IsNullOrEmpty(parameter))
                    {
                        communicate += $" obecnie {parameter}";
                    }
                }
                return communicate;
            }
        }
    
        public event Action<string>? OnStringSelected;

        public async Task Execute()
        {
            Console.WriteLine($"Podaj {Name}");
            string input = Console.ReadLine() ?? string.Empty;
            OnStringSelected?.Invoke(input);
        }
    }
}

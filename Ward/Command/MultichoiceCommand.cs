using Ward.Command.Executioner;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Command
{

    internal class MultichoiceCommand<T> : ICommand
    {
        private Multichoice<T> multichoice;

        public string Name => multichoice.Name;

        public string Description
        {
            get
            {
                string communicate = multichoice.DefaultDescription;
                var parameter = multichoice.ParameterSupplier();
                if (multichoice.ParameterValidator?.Invoke(parameter) ?? true)
                {
                    communicate += $" obecnie {ValueStr(parameter)}";
                }
                return communicate;
            }
        }

        public event Action<T>? OnValueSelected;

        public MultichoiceCommand(Multichoice<T> multichoice)
        {
            this.multichoice = multichoice;
        }

        public async Task Execute()
        {
            Console.WriteLine($"Wybierz {Name}: ");
            var values = multichoice.Values.Invoke();
            DisplayValues(values);
            Console.Write("Podaj odpowiedni numer: ");
            if (int.TryParse(Console.ReadLine(), out int selectedOption) && selectedOption > 0 && selectedOption <= values.Count)
            {
                var selectedValue = values[selectedOption - 1];
                Console.WriteLine($"Wybrano: {ValueStr(selectedValue)}");
                OnValueSelected?.Invoke(selectedValue);
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }

        private void DisplayValues(List<T> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ValueStr(values[i])}");
            }
        }

        private string ValueStr(T value)
        {
            return multichoice.ParameterTransformator?.Invoke(value) ?? value.ToString() ?? string.Empty;
        }
    }
}

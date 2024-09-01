using Emergency.Command.Executioner;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class SelectWardCommand : ICommand
    {
        private Func<WardType?>? _parameterSupplier;

        public string Name => "Oddział";

        public string Description
        {
            get
            {
                string communicate = $"Wybierz oddział";
                if (_parameterSupplier != null)
                {
                    var parameter = _parameterSupplier();
                    if (parameter != null)
                    {
                        communicate += $" obecnie {_parameterSupplier()?.ToPolish()}";
                    }
                }
                return communicate;
            }
        }

        public event Action<WardType>? OnWardSelected;

        public SelectWardCommand(Func<WardType?> parameterSupplier)
        {
            _parameterSupplier = parameterSupplier;
        }

        public async Task Execute()
        {
            Console.WriteLine($"Wybierz oddział: ");
            var wardValues = Enum.GetValues(typeof(WardType)).Cast<WardType>().ToList();
            DisplayWards(wardValues);

            Console.Write("Podaj numer odpowiadający oddziałowi: ");
            if (int.TryParse(Console.ReadLine(), out int selectedOption) && selectedOption > 0 && selectedOption <= wardValues.Count)
            {
                WardType selectedWard = wardValues[selectedOption - 1];
                Console.WriteLine($"Wybrałeś oddział: {selectedWard.ToPolish()}");
                OnWardSelected?.Invoke(selectedWard);
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }

        private void DisplayWards(List<WardType> wardValues)
        {
            for (int i = 0; i < wardValues.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {wardValues[i].ToPolish()}");
            }
        }
    }
}

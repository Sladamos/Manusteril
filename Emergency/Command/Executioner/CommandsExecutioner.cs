using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.Executioner
{
    internal class CommandsExecutioner : ICommandsExecutioner
    {
        public void Execute(Dictionary<string, ICommand> commands)
        {
            DisplayOptions(commands);
            Console.WriteLine("Wybierz opcję");
            string? commandName = Console.ReadLine();
            if (commandName != null && commandName != "" && commands.TryGetValue(commandName.Capitalize(), out ICommand? command))
            {
                command.Execute();
            }
            else
            {
                Console.WriteLine($"Niepoprawna opcja: {commandName}");
            }
        }

        private void DisplayOptions(Dictionary<string, ICommand> commands)
        {
            Console.WriteLine("");
            foreach (var item in commands)
            {
                Console.WriteLine($"[{item.Key}] {item.Value.Description}");
            }
            Console.WriteLine("");
        }
    }
}

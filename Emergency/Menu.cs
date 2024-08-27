using Emergency.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency
{
    internal class Menu : IMenu
    {

        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        public Menu(ICommandsFactory commandsFactory) {
            ExitProgramCommand exitProgramCommand = commandsFactory.CreateExitProgramCommand();
            CheckInsuranceCommand checkInsuranceCommand = commandsFactory.CheckInsuranceCommand();
            DeletePatientCommand deletePatientCommand = commandsFactory.DeletePatientCommand();
            AddPatientCommand addPatientCommand = commandsFactory.AddPatientCommand();
            commands[exitProgramCommand.Name] = exitProgramCommand;
            commands[checkInsuranceCommand.Name] = checkInsuranceCommand;
            commands[deletePatientCommand.Name] = deletePatientCommand;
            commands[addPatientCommand.Name] = addPatientCommand;
            exitProgramCommand.ProgramExited += () => enabled = false;
        }

        public void Start()
        {
            enabled = true;
            while (enabled)
            {
                DisplayOptions();
                Console.WriteLine("Wybierz opcję");
                string? commandName = Console.ReadLine();
                if (commandName != null && commands.TryGetValue(commandName, out ICommand? command))
                {
                    command.Execute();
                } else
                {
                    Console.WriteLine($"NIepoprawna opcja: {commandName}");
                }
            }
        }

        private void DisplayOptions()
        {
            foreach (var item in commands) {
                Console.WriteLine($"[{item.Key}] {item.Value.Description}");
            }
        }
    }
}

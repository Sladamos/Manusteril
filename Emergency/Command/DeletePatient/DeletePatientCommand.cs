using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;

namespace Emergency.Command.DeletePatient
{
    internal class DeletePatientCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private string pesel = "";

        public DeletePatientCommand(ICommandsFactory commandsFactory, ICommandsExecutioner commandsExecutioner)
        {
            this.commandsExecutioner = commandsExecutioner;
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[exitOptionCommand.Name] = exitOptionCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
        }

        public string Name => "Wypisz";

        public string Description => "Wypisz pacjenta";

        public void Execute()
        {
            Console.WriteLine("Usuwanie pacjenta");
            enabled = true;
            while (enabled)
            {
                commandsExecutioner.Execute(commands);
            }
        }

        private void OnPeselSelected(string pesel)
        {
            this.pesel = pesel;
        }

        private string GetPesel() { return pesel; }
    }
}

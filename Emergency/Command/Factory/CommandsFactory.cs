using Emergency.Command.DeletePatient;
using Emergency.Command.Executioner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.Factory
{
    internal class CommandsFactory : ICommandsFactory
    {
        private readonly ICommandsExecutioner commandsExecutioner;

        public CommandsFactory(ICommandsExecutioner commandsExecutioner) {
            this.commandsExecutioner = commandsExecutioner;
        }

        public ExitProgramCommand ExitProgramCommand() { return new ExitProgramCommand(); }

        public ExitOptionCommand ExitOptionCommand() { return new ExitOptionCommand(); }

        public DeletePatientCommand DeletePatientCommand() { return new DeletePatientCommand(this, commandsExecutioner); }

        public AddPatientCommand AddPatientCommand() { return new AddPatientCommand(); }

        public CheckInsuranceCommand CheckInsuranceCommand() { return new CheckInsuranceCommand(); }

        public SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier)
        {
            if (parameter == null || parameter.Length == 0)
            {
                throw new ArgumentException("Parameter for select string command must be specified");
            }
            return new SelectStringCommand(parameter, paremeterSupplier);
        }
    }
}

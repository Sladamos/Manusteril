using Emergency.Bus;
using Emergency.Command.DeletePatient;
using Emergency.Command.Executioner;
using Emergency.Patient;
using Emergency.Validator;
using log4net;
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

        private readonly IPatientService patientService;

        private readonly IValidatorService validator;

        public CommandsFactory(ICommandsExecutioner commandsExecutioner,
            IPatientService patientService,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.patientService = patientService;
            this.validator = validator;
        }

        public ExitProgramCommand ExitProgramCommand() { return new ExitProgramCommand(); }

        public ExitOptionCommand ExitOptionCommand() { return new ExitOptionCommand(); }

        public UnregisterPatientCommand DeletePatientCommand() { return new UnregisterPatientCommand(this, commandsExecutioner, validator); }

        public UnregisterPatientLogicCommand DeletePatientExecutionCommand(Func<string> peselSupplier) { return new UnregisterPatientLogicCommand(patientService, peselSupplier); }

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

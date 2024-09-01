using Emergency.Bus;
using Emergency.Command.CheckInsurance;
using Emergency.Command.DeletePatient;
using Emergency.Command.Executioner;
using Emergency.Patient;
using Emergency.Validator;
using Emergency.Visit;
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

        private readonly IVisitService visitService;

        private readonly IValidatorService validator;

        private readonly IPatientService patientService;

        public CommandsFactory(ICommandsExecutioner commandsExecutioner,
            IVisitService visitService,
            IValidatorService validator,
            IPatientService patientService)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.visitService = visitService;
            this.validator = validator;
            this.patientService = patientService;
        }

        public ExitProgramCommand ExitProgramCommand() 
        { 
            return new ExitProgramCommand(); 
        }

        public ExitOptionCommand ExitOptionCommand()
        { 
            return new ExitOptionCommand(); 
        }

        public UnregisterPatientCommand UnregisterPatientCommand() 
        { 
            return new UnregisterPatientCommand(this, commandsExecutioner, validator); 
        }

        public UnregisterPatientLogicCommand UnregisterPatientLogicCommand(Func<string> peselSupplier) 
        { 
            return new UnregisterPatientLogicCommand(visitService, peselSupplier); 
        }

        public AddPatientCommand AddPatientCommand() 
        { 
            return new AddPatientCommand(); 
        }

        public CheckInsuranceCommand CheckInsuranceCommand() 
        { 
            return new CheckInsuranceCommand(this, commandsExecutioner, validator); 
        }

        public CheckInsuranceLogicCommand CheckInsuranceLogicCommand(Func<string> getPesel)
        {
            return new CheckInsuranceLogicCommand(patientService, getPesel);
        }

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

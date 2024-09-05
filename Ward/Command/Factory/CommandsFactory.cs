using Ward.Bus;
using Ward.Command.Executioner;
using Ward.Patient;
using Ward.Validator;
using Ward.Visit;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;

namespace Ward.Command.Factory
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

        public SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier)
        {
            if (parameter == null || parameter.Length == 0)
            {
                throw new ArgumentException("Parameter for select string command must be specified");
            }
            return new SelectStringCommand(parameter, paremeterSupplier);
        }

        public MultichoiceCommand<WardType> SelectWardCommand(Multichoice<WardType> multichoice)
        {
            return new MultichoiceCommand<WardType>(multichoice);
        }
    }
}

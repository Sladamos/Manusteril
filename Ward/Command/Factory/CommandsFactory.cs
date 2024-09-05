using Ward.Bus;
using Ward.Command.CheckInsurance;
using Ward.Command.UnregisterPatient;
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
using Ward.Command.RegisterPatient;
using Messages;
using Ward.Command.AddPatient;
using Ward.Command.EditPatient;

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

        public UnregisterPatientCommand UnregisterPatientCommand() 
        { 
            return new UnregisterPatientCommand(this, commandsExecutioner, validator);
        }

        public RegisterPatientCommand RegisterPatientCommand()
        {
            return new RegisterPatientCommand(this, commandsExecutioner, validator);
        }

        public UnregisterPatientLogicCommand UnregisterPatientLogicCommand(Func<string> peselSupplier) 
        { 
            return new UnregisterPatientLogicCommand(visitService, peselSupplier); 
        }

        public AddPatientCommand AddPatientCommand() 
        { 
            return new AddPatientCommand(this, commandsExecutioner, validator);
        }

        public EditPatientCommand EditPatientCommand()
        {
            return new EditPatientCommand(this, commandsExecutioner, validator, patientService);
        }

        public AddPatientLogicCommand AddPatientLogicCommand(PatientInfo patientInfo)
        {
            return new AddPatientLogicCommand(patientService, patientInfo);
        }

        public EditPatientLogicCommand EditPatientLogicCommand(PatientInfo patientInfo)
        {
            return new EditPatientLogicCommand(patientService, patientInfo);
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

        public MultichoiceCommand<WardType> SelectWardCommand(Multichoice<WardType> multichoice)
        {
            return new MultichoiceCommand<WardType>(multichoice);
        }

        public RegisterPatientLogicCommand RegisterPatientLogicCommand(Func<string> getPesel, Func<WardType> getWard)
        {
            return new RegisterPatientLogicCommand(visitService, patientService, getPesel, getWard);
        }
    }
}

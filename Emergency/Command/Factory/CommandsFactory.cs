using Emergency.Bus;
using Emergency.Command.CheckInsurance;
using Emergency.Command.UnregisterPatient;
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
using Emergency.Command.RegisterPatient;
using Messages;
using Emergency.Command.AddPatient;
using Emergency.Command.EditPatient;
using Emergency.Command.AskForRegistration;

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

        public MultichoiceCommand<WardType> SelectWardCommand(Func<WardType> wardSupplier)
        {
            Multichoice<WardType> selectWardMultichoice = new()
            {
                Name = "Oddział",
                DefaultDescription = "Wybierz oddział",
                ParameterSupplier = wardSupplier,
                Values = Enum.GetValues(typeof(WardType)).Cast<WardType>().Where(ward => ward != WardType.NONE).ToList(),
                ParameterTransformator = (ward) => ward.ToPolish(),
                ParameterValidator = (ward) => ward != WardType.NONE
            };
            return new MultichoiceCommand<WardType>(selectWardMultichoice);
        }

        public RegisterPatientLogicCommand RegisterPatientLogicCommand(Func<string> getPesel, Func<WardType> getWard, Func<string> wardIdentifierSupplier)
        {
            return new RegisterPatientLogicCommand(visitService, patientService, getPesel, getWard, wardIdentifierSupplier);
        }

        public AskForRegistrationCommand AskForRegistrationCommand()
        {
            return new AskForRegistrationCommand(this, commandsExecutioner, validator);
        }

        public AskForRegistrationLogicCommand AskForRegistrationLogicCommand(Func<string> getPesel, Func<WardType> getWard)
        {
            return new AskForRegistrationLogicCommand(visitService, getPesel, getWard);
        }
    }
}

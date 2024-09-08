using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.AddPatient;
using Emergency.Command.AskForRegistration;
using Emergency.Command.CheckInsurance;
using Emergency.Command.EditPatient;
using Emergency.Command.RegisterPatient;
using Emergency.Command.UnregisterPatient;
using Emergency.Patient;
using Messages;

namespace Emergency.Command.Factory
{
    internal interface ICommandsFactory
    {
        ExitProgramCommand ExitProgramCommand();
        ExitOptionCommand ExitOptionCommand();
        UnregisterPatientCommand UnregisterPatientCommand();
        UnregisterPatientLogicCommand UnregisterPatientLogicCommand(Func<string> peselSupplier);
        AddPatientCommand AddPatientCommand();
        AddPatientLogicCommand AddPatientLogicCommand(PatientInfo patientInfo);
        EditPatientCommand EditPatientCommand();
        EditPatientLogicCommand EditPatientLogicCommand(PatientInfo patientInfo);
        CheckInsuranceCommand CheckInsuranceCommand();
        CheckInsuranceLogicCommand CheckInsuranceLogicCommand(Func<string> getPesel);
        SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier);
        MultichoiceCommand<WardType> SelectWardCommand(Func<WardType> wardSupplier);
        RegisterPatientCommand RegisterPatientCommand();
        RegisterPatientLogicCommand RegisterPatientLogicCommand(Func<string> getPesel, Func<WardType> getWard, Func<string> wardIdentifierSupplier);
        AskForRegistrationCommand AskForRegistrationCommand();
        AskForRegistrationLogicCommand AskForRegistrationLogicCommand(Func<string> getPesel, Func<WardType> getWard);
    }
}

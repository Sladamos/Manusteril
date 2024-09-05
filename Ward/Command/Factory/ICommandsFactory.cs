using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Command.AddPatient;
using Ward.Command.CheckInsurance;
using Ward.Command.EditPatient;
using Ward.Command.RegisterPatient;
using Ward.Command.UnregisterPatient;
using Ward.Patient;
using Messages;

namespace Ward.Command.Factory
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
        MultichoiceCommand<WardType> SelectWardCommand(Multichoice<WardType> multichoice);
        RegisterPatientCommand RegisterPatientCommand();
        RegisterPatientLogicCommand RegisterPatientLogicCommand(Func<string> getPesel, Func<WardType> getWard);
    }
}

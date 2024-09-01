using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.CheckInsurance;
using Emergency.Command.RegisterPatient;
using Emergency.Command.UnregisterPatient;
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
        CheckInsuranceCommand CheckInsuranceCommand();
        CheckInsuranceLogicCommand CheckInsuranceLogicCommand(Func<string> getPesel);
        SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier);
        SelectWardCommand SelectWardCommand(Func<WardType?> paremeterSupplier);
        RegisterPatientCommand RegisterPatientCommand();
        RegisterPatientLogicCommand RegisterPatientLogicCommand(Func<string> getPesel, Func<WardType?> getWard);
    }
}

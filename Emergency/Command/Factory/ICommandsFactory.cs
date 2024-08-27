using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.DeletePatient;

namespace Emergency.Command.Factory
{
    internal interface ICommandsFactory
    {
        ExitProgramCommand ExitProgramCommand();
        ExitOptionCommand ExitOptionCommand();
        DeletePatientCommand DeletePatientCommand();
        AddPatientCommand AddPatientCommand();
        CheckInsuranceCommand CheckInsuranceCommand();
        SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier);
    }
}

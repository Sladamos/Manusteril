using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal interface ICommandsFactory
    {
        ExitProgramCommand CreateExitProgramCommand();
        DeletePatientCommand DeletePatientCommand();
        AddPatientCommand AddPatientCommand();
        CheckInsuranceCommand CheckInsuranceCommand();
    }
}

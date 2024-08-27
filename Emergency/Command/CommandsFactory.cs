using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class CommandsFactoryImpl : ICommandsFactory
    {
        public ExitProgramCommand CreateExitProgramCommand() {  return new ExitProgramCommand(); }

        public DeletePatientCommand DeletePatientCommand() {  return new DeletePatientCommand(); }

        public AddPatientCommand AddPatientCommand() { return new AddPatientCommand();}

        public CheckInsuranceCommand CheckInsuranceCommand() {  return new CheckInsuranceCommand(); }
    }
}

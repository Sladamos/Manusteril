using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;
using Messages;
using Ward.Command.DisplayRoomOccupation;

namespace Ward.Command.Factory
{
    internal interface ICommandsFactory
    {
        ExitProgramCommand ExitProgramCommand();
        ExitOptionCommand ExitOptionCommand();
        DisplayRoomOccupationCommand DisplayRoomOccupation();
        SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier);
        MultichoiceCommand<WardType> SelectWardCommand(Multichoice<WardType> multichoice);
    }
}

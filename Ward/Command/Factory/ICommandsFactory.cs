using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;
using Messages;
using Ward.Command.DisplayRoomOccupation;
using Ward.Command.Patients;
using Ward.Command.Patients.FindPatientRoom;

namespace Ward.Command.Factory
{
    internal interface ICommandsFactory
    {
        ExitProgramCommand ExitProgramCommand();
        ExitOptionCommand ExitOptionCommand();
        DisplayRoomOccupationCommand DisplayRoomOccupationCommand();
        DisplayFreeRoomsCommand DisplayFreeRoomsCommand();
        SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier);
        MultichoiceCommand<WardType> SelectWardCommand(Multichoice<WardType> multichoice);
        PatientCommands CreatePatientsCommands();
        FindPatientRoomLogicCommand FindPatientRoomLogicCommand(Func<string> getPesel);
        FindPatientRoomCommand FindPatientRoomCommand();
    }
}

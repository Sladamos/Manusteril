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
using Ward.Command.Patients.ChangePatientRoom;
using Ward.Command.Patients.ChangePatientRoom;
using Ward.Command.Patients.AllowPatientToLeave;
using Ward.Command.Executioner;
using Ward.Validator;

namespace Ward.Command.Factory
{
    internal interface ICommandsFactory
    {
        ExitProgramCommand ExitProgramCommand();
        ExitOptionCommand ExitOptionCommand();
        DisplayRoomOccupationCommand DisplayRoomOccupationCommand();
        DisplayFreeRoomsCommand DisplayFreeRoomsCommand();
        SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier);
        MultichoiceCommand<string> SelectRoomCommand(Multichoice<string> multichoice);
        PatientCommands CreatePatientsCommands();
        FindPatientRoomLogicCommand FindPatientRoomLogicCommand(Func<string> getPesel);
        FindPatientRoomCommand FindPatientRoomCommand();
        ChangePatientRoomCommand ChangePatientRoomCommand();
        ChangePatientRoomLogicCommand ChangePatientRoomLogicCommand(Func<string> getPesel, Func<string> getRoomNumber);
        AllowPatientToLeaveCommand AllowPatientToLeaveCommand();
        AllowPatientToLeaveLogicCommand AllowPatientToLeaveLogicCommand(Func<string> peselSupplier, Func<string> pwzSupplier, Func<bool> ownRiskSupplier);
        MultichoiceCommand<bool> LeavedAtOwnRiskCommand(Multichoice<bool> multichoice);
    }
}

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
using Ward.Command.Patients.AllowPatientToLeave;
using Ward.Command.Executioner;
using Ward.Validator;
using Ward.Command.Patients.ConfirmPatientArrival;
using Ward.Command.Patients.ConsidierPatientAcceptance;

namespace Ward.Command.Factory
{
    internal interface ICommandsFactory
    {
        ExitProgramCommand ExitProgramCommand();
        ExitOptionCommand ExitOptionCommand();
        DisplayRoomOccupationCommand DisplayRoomOccupationCommand();
        DisplayFreeRoomsCommand DisplayFreeRoomsCommand();
        SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier);
        MultichoiceCommand<string> SelectRoomCommand(Func<string> GetRoomNumber);
        PatientCommands CreatePatientsCommands();
        FindPatientRoomLogicCommand FindPatientRoomLogicCommand(Func<string> getPesel);
        FindPatientRoomCommand FindPatientRoomCommand();
        ChangePatientRoomCommand ChangePatientRoomCommand();
        ChangePatientRoomLogicCommand ChangePatientRoomLogicCommand(Func<string> getPesel, Func<string> getRoomNumber);
        AllowPatientToLeaveCommand AllowPatientToLeaveCommand();
        AllowPatientToLeaveLogicCommand AllowPatientToLeaveLogicCommand(Func<string> peselSupplier, Func<string> pwzSupplier, Func<bool> ownRiskSupplier);
        MultichoiceCommand<bool> LeavedAtOwnRiskCommand(Func<bool> parameterSupplier);
        ConfirmPatientArrivalLogicCommand ConfirmPatientArrivalLogicCommand(Func<string> getPesel, Func<string> getRoomNumber);
        ConfirmPatientArrivalCommand ConfirmPatientArrivalCommand();
        MultichoiceCommand<string> SelectVisitQuestionsCommand(Func<string> peselProvider);
        MultichoiceCommand<bool> SelectVisitQuestionDecisionCommand(Func<bool> value);
        ConsiderPatientAcceptanceLogicCommand ConsiderPatientAcceptanceLogicCommand(Func<string> getPesel, Func<bool> value, Func<string> getReason);
        ConsiderPatientAcceptanceCommand ConsiderPatientAcceptanceCommand();
    }
}

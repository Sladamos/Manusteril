﻿using Ward.Bus;
using Ward.Command.Executioner;
using Ward.Patient;
using Ward.Validator;
using Ward.Visit;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Ward.Command.DisplayRoomOccupation;
using Ward.Room;
using Ward.Command.Patients;
using Ward.Command.Patients.FindPatientRoom;
using Ward.Command.Patients.ChangePatientRoom;
using Ward.Command.Patients.ChangePatientRoom;
using Ward.Command.Patients.AllowPatientToLeave;

namespace Ward.Command.Factory
{
    internal class CommandsFactory : ICommandsFactory
    {
        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private readonly IRoomService roomService;

        private readonly IVisitService visitService;

        private readonly IPatientService patientService;

        public CommandsFactory(ICommandsExecutioner commandsExecutioner,
            IValidatorService validator,
            IRoomService roomService,
            IVisitService visitService,
            IPatientService patientService)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            this.roomService = roomService;
            this.visitService = visitService;
            this.patientService = patientService;
        }

        public ExitProgramCommand ExitProgramCommand() 
        { 
            return new ExitProgramCommand(); 
        }

        public ExitOptionCommand ExitOptionCommand()
        { 
            return new ExitOptionCommand(); 
        }

        public SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier)
        {
            if (parameter == null || parameter.Length == 0)
            {
                throw new ArgumentException("Parameter for select string command must be specified");
            }
            return new SelectStringCommand(parameter, paremeterSupplier);
        }

        public MultichoiceCommand<WardType> SelectWardCommand(Multichoice<WardType> multichoice)
        {
            return new MultichoiceCommand<WardType>(multichoice);
        }

        public DisplayRoomOccupationCommand DisplayRoomOccupationCommand()
        {
            return new DisplayRoomOccupationCommand(roomService);
        }

        public DisplayFreeRoomsCommand DisplayFreeRoomsCommand()
        {
            return new DisplayFreeRoomsCommand(roomService);
        }

        public PatientCommands CreatePatientsCommands()
        {
            return new PatientCommands(this, commandsExecutioner);
        }

        public FindPatientRoomLogicCommand FindPatientRoomLogicCommand(Func<string> getPesel)
        {
            return new FindPatientRoomLogicCommand(roomService, getPesel);
        }

        public FindPatientRoomCommand FindPatientRoomCommand()
        {
            return new FindPatientRoomCommand(this, commandsExecutioner, validator);
        }

        public MultichoiceCommand<string> SelectRoomCommand(Multichoice<string> multichoice)
        {
            return new MultichoiceCommand<string>(multichoice);
        }

        public ChangePatientRoomCommand ChangePatientRoomCommand()
        {
            return new ChangePatientRoomCommand(this, commandsExecutioner, roomService, validator);
        }

        public ChangePatientRoomLogicCommand ChangePatientRoomLogicCommand(Func<string> getPesel, Func<string> getRoomNumber)
        {
            return new ChangePatientRoomLogicCommand(roomService, patientService, getPesel, getRoomNumber);
        }

        public AllowPatientToLeaveLogicCommand AllowPatientToLeaveLogicCommand(Func<string> peselSupplier, Func<string> pwzSupplier, Func<bool> ownRiskSupplier)
        {
            return new AllowPatientToLeaveLogicCommand(visitService, peselSupplier, pwzSupplier, ownRiskSupplier);
        }

        public AllowPatientToLeaveCommand AllowPatientToLeaveCommand()
        {
            return new AllowPatientToLeaveCommand(this, commandsExecutioner, validator);
        }

        public MultichoiceCommand<bool> LeavedAtOwnRiskCommand(Func<bool> parameterSupplier)
        {
            Multichoice<bool> multichoice = new()
            {
                Values = () => [true, false],
                DefaultDescription = "Czy na własne żądanie",
                Name = "Żądanie",
                ParameterSupplier = parameterSupplier,
                ParameterTransformator = (value) => value ? "tak" : "nie"
            };
            return new MultichoiceCommand<bool>(multichoice);
        }
    }
}

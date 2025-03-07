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
using Ward.Command.Patients.AllowPatientToLeave;
using Ward.Command.Patients.ConfirmPatientArrival;
using Ward.Visit.Question;
using Ward.Command.Patients.ConsidierPatientAcceptance;

namespace Ward.Command.Factory
{
    internal class CommandsFactory : ICommandsFactory
    {
        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private readonly IRoomService roomService;

        private readonly IVisitService visitService;

        private readonly IPatientService patientService;

        private readonly IVisitQuestionService visitQuestionService;

        public CommandsFactory(ICommandsExecutioner commandsExecutioner,
            IValidatorService validator,
            IRoomService roomService,
            IVisitService visitService,
            IVisitQuestionService visitQuestionService,
            IPatientService patientService)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            this.roomService = roomService;
            this.visitService = visitService;
            this.visitQuestionService = visitQuestionService;
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

        public MultichoiceCommand<string> SelectRoomCommand(Func<string> GetRoomNumber)
        {
            Multichoice<string> selectRoomMultichoice = new()
            {
                Values = () => roomService.GetAll().Where(room => room.OccupiedBeds < room.Capacity).Select(room => room.Number).ToList(),
                DefaultDescription = "Wybierz salę",
                Name = "Sala",
                ParameterSupplier = GetRoomNumber,
                ParameterValidator = (room) => !string.IsNullOrEmpty(room)
            };

            return new MultichoiceCommand<string>(selectRoomMultichoice);
        }

        public ChangePatientRoomCommand ChangePatientRoomCommand()
        {
            return new ChangePatientRoomCommand(this, commandsExecutioner, roomService, validator);
        }

        public ChangePatientRoomLogicCommand ChangePatientRoomLogicCommand(Func<string> getPesel, Func<string> getRoomNumber)
        {
            return new ChangePatientRoomLogicCommand(roomService, patientService, visitService, getPesel, getRoomNumber);
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
                ParameterTransformator = (value) => value ? "Tak" : "Nie"
            };
            return new MultichoiceCommand<bool>(multichoice);
        }

        public ConfirmPatientArrivalLogicCommand ConfirmPatientArrivalLogicCommand(Func<string> getPesel, Func<string> getRoomNumber)
        {
            return new ConfirmPatientArrivalLogicCommand(visitService, getPesel, getRoomNumber);
        }

        public ConfirmPatientArrivalCommand ConfirmPatientArrivalCommand()
        {
            return new ConfirmPatientArrivalCommand(this, commandsExecutioner, validator);
        }

        public MultichoiceCommand<string> SelectVisitQuestionsCommand(Func<string> peselProvider)
        {
            Multichoice<string> multichoice = new()
            {
                DefaultDescription = "Wybierz pytanie o wizytę",
                Values = () => this.visitQuestionService.GetQuestions()
                .Where(question => !question.Answered)
                .Select(question => question.PatientPesel)
                .ToList(),
                Name = "Pytanie",
                ParameterSupplier = peselProvider,
                ParameterValidator = (pesel) => !string.IsNullOrEmpty(pesel)
            };
            return new MultichoiceCommand<string>(multichoice);
        }

        public MultichoiceCommand<bool> SelectVisitQuestionDecisionCommand(Func<bool> parameterSupplier)
        {
            Multichoice<bool> multichoice = new()
            {
                Values = () => [true, false],
                DefaultDescription = "Czy można przyjąć pacjenta",
                Name = "Decyzja",
                ParameterSupplier = parameterSupplier,
                ParameterTransformator = (value) => value ? "Tak" : "Nie"
            };
            return new MultichoiceCommand<bool>(multichoice);
        }

        public ConsiderPatientAcceptanceLogicCommand ConsiderPatientAcceptanceLogicCommand(Func<string> getPesel, Func<bool> value, Func<string> getReason)
        {
            return new ConsiderPatientAcceptanceLogicCommand(visitQuestionService, getPesel, value, getReason);
        }

        public ConsiderPatientAcceptanceCommand ConsiderPatientAcceptanceCommand()
        {
            return new ConsiderPatientAcceptanceCommand(this, commandsExecutioner, validator);
        }
    }
}

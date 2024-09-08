using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Command.DisplayRoomOccupation;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using Ward.Command.Patients.ConfirmPatientArrival;
using Ward.Validator;

namespace Ward.Command.Patients.ConsidierPatientAcceptance
{
    internal class ConsiderPatientAcceptanceCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private string pesel = "";

        private string reason = "";

        private bool decision = false;

        public string Name => "Pytania";

        public string Description => "Wyświetl pytania o przyjęcie pacjentów";

        public ConsiderPatientAcceptanceCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            DisplayFreeRoomsCommand displayFreeRoomsCommand = commandsFactory.DisplayFreeRoomsCommand();
            ConsiderPatientAcceptanceLogicCommand considerPatientAcceptanceLogicCommand = commandsFactory.ConsiderPatientAcceptanceLogicCommand(GetPesel, () => decision, GetReason);
            MultichoiceCommand<string> selectPeselCommand = commandsFactory.SelectVisitQuestionsCommand(GetPesel);
            MultichoiceCommand<bool> selectDecisionCommand = commandsFactory.SelectVisitQuestionDecisionCommand(() => decision);
            SelectStringCommand selectReasonCommand = commandsFactory.SelectStringCommand("Powód odmowy", GetReason);
            Condition reasonCommandCondition = new () { Reason = "Nie odmówiono przyjęcia", Predicate = () => !decision };
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[selectDecisionCommand.Name] = selectDecisionCommand;
            commands[selectReasonCommand.Name] = new ConditionalCommand(selectReasonCommand, reasonCommandCondition);
            commands[displayFreeRoomsCommand.Name] = displayFreeRoomsCommand;
            commands[considerPatientAcceptanceLogicCommand.Name] = considerPatientAcceptanceLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnValueSelected += OnPeselSelected;
            selectDecisionCommand.OnValueSelected += OnDecisionSelected;
            selectReasonCommand.OnStringSelected += OnReasonSelected;
            considerPatientAcceptanceLogicCommand.OnAnswerSent = OnAnswerSent;
        }

        private void OnAnswerSent()
        {
            reason = "";
        }

        private void OnDecisionSelected(bool decision)
        {
            this.decision = decision;
            reason = "";
        }

        public async Task Execute()
        {
            Console.WriteLine("Wyświetlanie zapytań");
            enabled = true;
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
            pesel = "";
            reason = "";
        }

        private void OnPeselSelected(string pesel)
        {
            var validationResult = validator.ValidatePesel(pesel);
            if (validationResult.IsValid)
            {
                this.pesel = pesel;
                reason = "";
            }
            else
            {
                Console.WriteLine(validationResult?.ValidatorMessage ?? "Niepoprawny PESEL");
            }
        }

        private void OnReasonSelected(string reason)
        {
            this.reason = reason;
        }

        private string GetPesel() { return pesel; }

        private string GetReason() { return reason; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;
using Emergency.Validator;
using Messages;

namespace Emergency.Command.RegisterPatient
{
    internal class RegisterPatientCommand : ICommand
    {
        private bool enabled = false;

        private Dictionary<string, ICommand> commands = [];

        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private string pesel = "";

        private WardType ward = WardType.NONE;

        public string Name => "Zarejestruj";

        public string Description => "Zarejestruj pacjenta na wizytę";

        public RegisterPatientCommand(ICommandsFactory commandsFactory,
            ICommandsExecutioner commandsExecutioner,
            IValidatorService validator)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            SelectStringCommand selectPeselCommand = commandsFactory.SelectStringCommand("PESEL", GetPesel);
            ExitOptionCommand exitOptionCommand = commandsFactory.ExitOptionCommand();
            Multichoice<WardType> selectWardMultichoice = new()
            {
                Name = "Oddział",
                DefaultDescription = "Wybierz oddział",
                ParameterSupplier = GetWard,
                Values = Enum.GetValues(typeof(WardType)).Cast<WardType>().Where(ward => ward != WardType.NONE).ToList(),
                ParameterTransformator = (ward) => ward.ToPolish(),
                ParameterValidator = (ward) => ward != WardType.NONE
            };
            MultichoiceCommand<WardType> selectWardCommand = commandsFactory.SelectWardCommand(selectWardMultichoice);
            RegisterPatientLogicCommand registerPatientLogicCommand = commandsFactory.RegisterPatientLogicCommand(GetPesel, GetWard);
            commands[selectPeselCommand.Name] = selectPeselCommand;
            commands[exitOptionCommand.Name] = exitOptionCommand;
            commands[selectWardCommand.Name] = selectWardCommand;
            commands[registerPatientLogicCommand.Name] = registerPatientLogicCommand;
            exitOptionCommand.OptionExited += () => enabled = false;
            selectPeselCommand.OnStringSelected += OnPeselSelected;
            selectWardCommand.OnValueSelected += OnWardSelected;
            registerPatientLogicCommand.OnPatientRegistered += OnPatientRegistered;
        }

        public async Task Execute()
        {
            Console.WriteLine("Wypisywanie pacjenta");
            enabled = true;
            while (enabled)
            {
                await commandsExecutioner.Execute(commands);
            }
            pesel = "";
            ward = WardType.NONE;
        }

        private void OnPeselSelected(string pesel)
        {
            var validationResult = validator.ValidatePesel(pesel);
            if (validationResult.IsValid)
            {
                this.pesel = pesel;
            } else
            {
                Console.WriteLine(validationResult?.ValidatorMessage ?? "Niepoprawny PESEL");
            }
        }

        private void OnWardSelected(WardType ward)
        {
            this.ward = ward;
        }

        private void OnPatientRegistered()
        {
            Console.WriteLine("Pomyślnie zarejestrowano pacjenta");
            enabled = false;
        }

        private string GetPesel() { return pesel; }

        private WardType GetWard() { return ward; }
    }
}

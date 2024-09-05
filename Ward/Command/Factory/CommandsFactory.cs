using Ward.Bus;
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

namespace Ward.Command.Factory
{
    internal class CommandsFactory : ICommandsFactory
    {
        private readonly ICommandsExecutioner commandsExecutioner;

        private readonly IValidatorService validator;

        private readonly IRoomService roomService;

        public CommandsFactory(ICommandsExecutioner commandsExecutioner,
            IValidatorService validator,
            IRoomService roomService)
        {
            this.commandsExecutioner = commandsExecutioner;
            this.validator = validator;
            this.roomService = roomService;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;
using Ward.Room;
using Ward.Visit;

namespace Ward.Command.Patients.FindPatientRoom
{
    internal class FindPatientRoomLogicCommand : ICommand
    {
        private IRoomService roomService;

        private Func<string> peselSupplier;

        public string Name => "Znajdź";

        public string Description => "Znajdź salę wybranego pacjenta";

        public Action<RoomEntity>? OnRoomFound;

        public FindPatientRoomLogicCommand(IRoomService roomService, Func<string> peselSupplier)
        {
            this.roomService = roomService;
            this.peselSupplier = peselSupplier;
        }

        public async Task Execute()
        {
            string pesel = peselSupplier();
            try
            {
                var room = roomService.GetRoomByPatientPesel(pesel);
                OnRoomFound?.Invoke(room);
            }
            catch (InvalidPeselException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnregisteredPatientException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Room;

namespace Ward.Command.DisplayRoomOccupation
{
    internal class DisplayFreeRoomsCommand : ICommand
    {
        private IRoomService roomService;

        public string Name => "Wypisz wolne";

        public string Description => "Wypisz wolne sale";

        public DisplayFreeRoomsCommand(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        public async Task Execute()
        {
            var rooms = roomService.GetAll().Where(room => room.Capacity > room.OccupiedBeds).OrderBy(room => room.Number).ToList();
            if(rooms.Count == 0)
            {
                Console.WriteLine("Brak wolnych sal");
            }
            else
            {
                DisplayRoomsContent(rooms);
            }
        }

        private void DisplayRoomsContent(List<RoomEntity> rooms)
        {
            foreach (var room in rooms)
            {
                Console.WriteLine($"Sala: {room.Number}, wolne łóżka: {room.Capacity - room.OccupiedBeds}");
            }
        }
    }
}

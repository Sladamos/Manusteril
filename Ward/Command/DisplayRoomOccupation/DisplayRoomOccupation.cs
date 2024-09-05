﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Room;

namespace Ward.Command.DisplayRoomOccupation
{
    internal class DisplayRoomOccupation : ICommand
    {
        private IRoomService roomService;

        public string Name => "Wypisz";

        public string Description => "Wypisz obłożenie sal";

        public DisplayRoomOccupation(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        public async Task Execute()
        {
            var rooms = roomService.GetAll();
            foreach (var room in rooms)
            {
                Console.WriteLine($"Sala: {room.Number}, zajęte łóżka: {room.OccupiedBeds}/{room.Capacity}");
            }
        }
    }
}

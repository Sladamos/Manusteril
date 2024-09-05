using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Config;
using Ward.Patient;
using Ward.Room;
using Ward.Visit;

namespace Ward
{
    internal class Initializer
    {
        private RoomRepository roomRepository;

        private ApplicationDbContext applicationDbContext;

        public Initializer(RoomRepository roomRepository, ApplicationDbContext applicationDbContext)
        {
            this.roomRepository = roomRepository;
            this.applicationDbContext = applicationDbContext;
        }

        public void Initialize()
        {
            applicationDbContext.Ensure();
            InitializeRooms();
        }

        private void InitializeRooms()
        {
            List<RoomEntity> rooms = [];
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "118", Capacity = 2, OccupiedBeds = 0, Patients = [] });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "119A", Capacity = 1, OccupiedBeds = 0, Patients = [] });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "119B", Capacity = 1, OccupiedBeds = 0, Patients = [] });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "115", Capacity = 4, OccupiedBeds = 0, Patients = [] });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "121", Capacity = 3, OccupiedBeds = 0, Patients = [] });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "143", Capacity = 6, OccupiedBeds = 0, Patients = [] });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "152A", Capacity = 2, OccupiedBeds = 0, Patients = [] });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "153B", Capacity = 2, OccupiedBeds = 0, Patients = [] });
            rooms.ForEach(room => roomRepository.Save(room));
        }
    }
}

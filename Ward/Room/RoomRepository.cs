using log4net.Core;
using Microsoft.EntityFrameworkCore;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Config;
using Ward.Visit;

namespace Ward.Room
{
    internal class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomEntity> GetAll()
        {
            return _context.Rooms.ToList();
        }

        /// <exception cref = "UnregisteredPatientException">
        /// Thrown when no room with a null RoomEndDate is found for the given patient.
        /// </exception>
        public RoomEntity GetRoomByPatientPesel(string pesel)
        {
            return GetAll()
                .FirstOrDefault(room => room.Patients
                                        .Split(";")
                                        .Contains(pesel))
                ?? throw new UnregisteredPatientException("Pacjent nie jest na wizycie w placówce");
        }

        public RoomEntity GetRoomByRoomNumber(string roomNumber)
        {
            return GetAll()
                .FirstOrDefault(room => room.Number == roomNumber)
                ?? throw new IncorrectRoomException("Niepoprawny numer sali");
        }

        public void Save(RoomEntity room)
        {
            SaveOrUpdate(room);
        }

        private void SaveOrUpdate(RoomEntity room)
        {
            using (var dbTrasaction = _context.Database.BeginTransaction())
            {
                var existingEntity = _context.Rooms
                .AsNoTracking()
                .FirstOrDefault(v => v.Id == room.Id);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(room);
                }
                else
                {
                    _context.Rooms.Add(room);
                }

                _context.SaveChanges();
                dbTrasaction.Commit();
            }
        }
    }
}

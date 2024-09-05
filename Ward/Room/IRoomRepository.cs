using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Room
{
    internal interface IRoomRepository
    {
        IEnumerable<RoomEntity> GetAll();
        RoomEntity GetRoomByPatientPesel(string pesel);
        RoomEntity GetRoomByRoomNumber(string roomNumber);
        void Save(RoomEntity room);
    }
}

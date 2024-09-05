using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;

namespace Ward.Room
{
    internal interface IRoomService
    {
        List<RoomEntity> GetAll();
        RoomEntity GetRoomByPatientPesel(string pesel);
        void AddPatientToRoom(PatientEntity patient, string roomNumber);
        void TransferPatientToRoom(PatientEntity patient, string roomNumber);
        void RemovePatientFromRoom(PatientEntity patient);
    }
}

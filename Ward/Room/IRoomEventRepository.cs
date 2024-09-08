using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;

namespace Ward.Room
{
    internal interface IRoomEventRepository
    {
        void NotifyAboutTransfer(PatientEntity patient, RoomEntity room);
    }
}

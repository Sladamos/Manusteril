using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;
using Ward.Visit;
using static MassTransit.MessageHeaders;

namespace Ward.Room
{
    internal class RoomEntity
    {
        public required Guid Id { get; set; }

        public required string Number { get; set; }

        public required int Capacity { get; set; }

        public required int OccupiedBeds { get; set; }

        public required ICollection<PatientEntity> Patients { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder("RoomEntity [");
            sb.Append($"Id={Id}, ");
            sb.Append($"Number={Number}, ");
            sb.Append($"Capacity={Capacity}, ");
            sb.Append($"OccupiedBeds={OccupiedBeds}, ");

            if (sb[sb.Length - 2] == ',')
                sb.Length -= 2;

            sb.Append("]");
            return sb.ToString();
        }
    }
}

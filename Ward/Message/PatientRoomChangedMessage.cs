using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Message
{
    internal class PatientRoomChangedMessage : IPatientWardRoomChangedMessage
    {
        public required Guid PatientId { get; set; }

        public required string PatientPesel { get; set; }

        public required string WardIdentifier {  get; set; }

        public required string Room {  get; set; }
    }
}

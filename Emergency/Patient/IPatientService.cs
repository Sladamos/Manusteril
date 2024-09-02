using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Patient
{
    internal interface IPatientService
    {
        PatientEntity GetPatientByPesel(string pesel);
        void AddPatient(PatientEntity patient);
        void EditPatient(PatientEntity patient);
        Task<IIsPatientInsuredResponse> IsPatientInsured(string pesel);
    }
}

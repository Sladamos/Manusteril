using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Patient
{
    internal interface IPatientService
    {
        Patient GetPatientByPesel(string pesel);
        void AddPatient(Patient patient);
        void DeletePatientByPesel(string pesel);
    }
}

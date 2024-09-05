﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Patient
{
    internal interface IPatientRepository
    {
        PatientEntity GetPatientByPesel(string pesel);
        void Save(PatientEntity visit);
    }
}

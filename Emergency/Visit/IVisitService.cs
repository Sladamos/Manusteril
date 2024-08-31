using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal interface IVisitService
    {
        void UnregisterPatientByPesel(string pesel);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal interface IVisitRepository
    {
        IEnumerable<VisitEntity> GetAll();
        IEnumerable<VisitEntity> GetAllByPatient(string pesel);
        VisitEntity GetPatientCurrentVisit(string pesel);
        void Save(VisitEntity visit);
    }
}

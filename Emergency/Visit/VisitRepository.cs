using Emergency.Config;
using Lombok.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal partial class VisitRepository : IVisitRepository
    {
        private readonly ApplicationDbContext _context;

        public VisitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<VisitEntity> GetAll()
        {
            return _context.Visits.ToList();
        }

        public IEnumerable<VisitEntity> GetAllByPatient(string pesel)
        {
            return GetAll().Where(visit => visit.PatientPesel == pesel);
        }
    }
}

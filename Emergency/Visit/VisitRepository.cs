using Emergency.Config;
using Lombok.NET;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal class VisitRepository : IVisitRepository
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

        public void Save(VisitEntity visit)
        {
           SaveOrUpdate(visit);
        }

        private void SaveOrUpdate(VisitEntity visit)
        {
            using(var dbTrasaction = _context.Database.BeginTransaction())
            {
                var existingEntity = _context.Visits
                .AsNoTracking()
                .FirstOrDefault(v => v.Id == visit.Id);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(visit);
                }
                else
                {
                    _context.Visits.Add(visit);
                }

                _context.SaveChanges();
                dbTrasaction.Commit();
            }
        }
    }
}

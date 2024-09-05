using Ward.Config;
using Ward.Visit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Patient
{
    internal class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <exception cref = "UnknownPatientException">
        /// Thrown when no patient with a Pesel is found.
        /// </exception>
        public PatientEntity GetPatientByPesel(string pesel)
        {
            return _context.Patients
                .FirstOrDefault(patient => patient.Pesel == pesel) ??
                throw new UnknownPatientException("Pacjent nieznany w placówce");
        }

        public void Save(PatientEntity patient)
        {
            SaveOrUpdate(patient);
        }

        private void SaveOrUpdate(PatientEntity patient)
        {
            using (var dbTrasaction = _context.Database.BeginTransaction())
            {
                var existingEntity = _context.Patients
                .AsNoTracking()
                .FirstOrDefault(v => v.Pesel == patient.Pesel);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(patient);
                }
                else
                {
                    _context.Patients.Add(patient);
                }

                _context.SaveChanges();
                dbTrasaction.Commit();
            }
        }
    }
}

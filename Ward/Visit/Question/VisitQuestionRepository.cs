using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Config;

namespace Ward.Visit.Question
{
    internal class VisitQuestionRepository : IVisitQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public VisitQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<VisitQuestionEntity> GetAll()
        {
            return _context.Questions.ToList();
        }

        public void Save(VisitQuestionEntity question)
        {
            SaveOrUpdate(question);
        }

        private void SaveOrUpdate(VisitQuestionEntity question)
        {
            using (var dbTrasaction = _context.Database.BeginTransaction())
            {
                var existingEntity = _context.Questions
                .AsNoTracking()
                .FirstOrDefault(v => v.Id == question.Id);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(question);
                }
                else
                {
                    _context.Questions.Add(question);
                }

                _context.SaveChanges();
                dbTrasaction.Commit();
            }
        }
    }
}

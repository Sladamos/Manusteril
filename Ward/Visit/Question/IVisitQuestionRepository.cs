using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Visit.Question
{
    internal interface IVisitQuestionRepository
    {
        void Save(VisitQuestionEntity question);
        IEnumerable<VisitQuestionEntity> GetAll();
    }
}

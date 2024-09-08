﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Visit.Question
{
    internal interface IVisitQuestionService
    {
        void AddQuestion(VisitQuestionEntity question);
        List<VisitQuestionEntity> GetQuestions();
    }
}

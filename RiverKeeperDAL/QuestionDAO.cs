﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDO;

namespace RiverKeeperDAL
{
    ///<summary>
    /// Marquette University
    /// MSCS 6931 701-702 Service-Oriented Architecture
    /// Spring 2018 
    /// 
    /// May 1st, 2018
    ///     
    /// Question data access layer object
    /// 
    ///</summary>
    public class QuestionDAO
    {
        //INFO: Return all questions in the database.
        public List<QuestionDO> GetQuestions(int id)
        {
            List<QuestionDO> questionDOs = new List<QuestionDO>();
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                List<Question> questions = (from q in RKEntities.Questions
                                            where q.SurveyId == id
                                            select q).ToList();
                foreach (var question in questions)
                {
                    if (question != null)
                    {
                        QuestionDO questionDO = new QuestionDO()
                        {
                            QuestionId = question.QuestionId,
                            Type = (short)question.Type,
                            Wording = question.Wording,
                            PossibleAnswers = (String.IsNullOrEmpty(question.PossibleAnswers)) ? new string[0] : question.PossibleAnswers.Split(','),
                            SurveyId = question.SurveyId
                        };
                        questionDOs.Add(questionDO);
                    }
                    if (question == null)
                    {
                        throw new Exception("No questions in DB");
                    }
                }

            }
            return questionDOs;
        }

        //INFO: Retrieves the question with the given id
        public QuestionDO GetQuestionById(int id)
        {
            QuestionDO questionDO = new QuestionDO();
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                List<Question> questions = (from q in RKEntities.Questions where q.QuestionId==id
                                            select q).ToList();
                Question question = questions.ElementAt(0);

                    if (question != null)
                    {
                        questionDO.QuestionId = question.QuestionId;
                        questionDO.Type = (short)question.Type;
                        questionDO.Wording = question.Wording;
                        questionDO.PossibleAnswers = question.PossibleAnswers.Split(',');
                    }
                    else
                    {
                        throw new Exception("No questions with that ID in DB");
                    }
                }
            return questionDO;
        }
            

        //INFO: Adds a question to the database.
        public bool CreateQuestion(QuestionDO questionDO)
        {
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                Question questiondb = new Question();

                questiondb.SurveyId = questionDO.SurveyId;
                questiondb.Wording = questionDO.Wording;
                questiondb.Type = (ResponseFormat)questionDO.Type;
                questiondb.PossibleAnswers = String.Join(",", questionDO.PossibleAnswers);
                RKEntities.Questions.Add(questiondb);
                

                int dbResult = 0;

                try
                {
                    dbResult = RKEntities.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

                if (dbResult != 1)
                {
                    return false;
                }

                return true;
            }
        }
    }
}

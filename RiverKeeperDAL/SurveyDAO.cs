using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDO;

namespace RiverKeeperDAL
{
    public class SurveyDAO
    {
        public List<SurveyDO> GetSurveys()
        {
            List<SurveyDO> surveyDOs = new List<SurveyDO>();

            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                List<Survey> surveys = (from s in RKEntities.Surveys
                                        orderby s.CreationDate
                                        select s).ToList();

                if (surveys.Count == 0)
                {
                    throw new Exception("No surveys in DB");
                }

                foreach (var survey in surveys)
                {
                    string email = (from u in RKEntities.Users
                                    where u.UserId.Equals(survey.UserId)
                                    select u.EmailAddress).FirstOrDefault();

                    SurveyDO surveyDO = new SurveyDO();
                    surveyDO.SurveyId = survey.SurveyId;
                    surveyDO.Name = survey.Name;
                    surveyDO.CreationDate = survey.CreationDate;
                    surveyDO.Email = (from u in RKEntities.Users
                                      where u.UserId.Equals(survey.UserId)
                                      select u.EmailAddress).FirstOrDefault();

                    int numberOfQuestions = survey.Answers.Count();

                    for (int i = 0; i < numberOfQuestions; i++)
                    {
                        AnswerDO answerDO = new AnswerDO();
                        Answer answerdb = survey.Answers.ElementAt(i);
                        answerDO.Response = answerdb.Response;
                        answerDO.QuestionId = answerdb.QuestionId;
                        surveyDO.Answers.Add(answerDO);
                    }
                    surveyDOs.Add(surveyDO);
                }
            }
            return surveyDOs;
        }

        //INFO: Return survey with given Id
        public SurveyDO GetSurvey(int id)
        {
            SurveyDO surveyDO = new SurveyDO();
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                List<Survey> surveys = (from s in RKEntities.Surveys where s.SurveyId == id
                                    select s).ToList();

                if(surveys.Count == 0)
                {
                    throw new Exception("No survey in DB with that ID");
                }
                Survey survey = surveys.ElementAt(0);

                surveyDO.SurveyId = survey.SurveyId;
                surveyDO.Name = survey.Name;
                surveyDO.CreationDate = survey.CreationDate;

                int numberOfQuestions = survey.Answers.Count();

                for (int i = 0; i < numberOfQuestions; i++)
                {
                    AnswerDO answerDO = new AnswerDO();
                    Answer answerdb = survey.Answers.ElementAt(i);
                    answerDO.Response = answerdb.Response;
                    answerDO.QuestionId = answerdb.QuestionId;
                    surveyDO.Answers.Add(answerDO);
                }
            }
            return surveyDO;
        }

        //INFO: Adds a new survey to the database
        public bool SubmitSurvey(SurveyDO surveyDO)
        {
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                Console.WriteLine("Attempting to submit survey");
                Survey surveydb = new Survey();

                if (surveyDO != null)
                {
                    
                    surveydb.Name = surveyDO.Name;
                    surveydb.CreationDate = DateTime.Now;
                    surveydb.UserId = (from u in RKEntities.Users
                                       where u.EmailAddress.Equals(surveyDO.Email)
                                       select u.UserId).FirstOrDefault();

                    int numberOfQuestions = surveyDO.Answers.Count();
                    
                    for(int i = 0; i < numberOfQuestions; i++)
                    {
                        AnswerDO answerDO = surveyDO.Answers.ElementAt(i);
                        Answer answerdb = new Answer();
                        answerdb.Response = answerDO.Response;
                        answerdb.QuestionId = answerDO.QuestionId;
                        surveydb.Answers.Add(answerdb);
                    }

                    RKEntities.Surveys.Add(surveydb);
                }

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
                catch(DbUpdateException e)
                {
                  
                    Debug.WriteLine(e.InnerException);
                }

                if (dbResult == 0)
                {
                    return false;
                }

                return true;
            }
        }
    }
}

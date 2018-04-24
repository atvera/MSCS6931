using System;
using System.Collections.Generic;
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
        //INFO: Return survey with Id
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
                surveyDO.isTemplate = survey.isTemplate;
            }
            return surveyDO;
        }

        public bool CreateSurvey(SurveyDO surveyDO)
        {
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                Survey surveydb = new Survey();

                if (surveyDO != null)
                {
                    surveydb.Name = surveyDO.Name;
                    surveydb.isTemplate = surveyDO.isTemplate;
                    surveydb.CreationDate = DateTime.Now;
                    surveydb.UserId = 1; //TODO: This will be set to the UserID that is logged in (filling the survey)
                    //surveydb.Questions.Add(RKEntities.Questions.Find(1));
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

                if (dbResult != 1)
                {
                    return false;
                }

                return true;
            }
        }
    }
}

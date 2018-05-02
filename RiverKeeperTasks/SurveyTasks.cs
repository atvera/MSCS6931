using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDO;
using RiverKeeperDAL;

namespace RiverKeeperTasks
{
    ///<summary>
    /// Marquette University
    /// MSCS 6931 701-702 Service-Oriented Architecture
    /// Spring 2018 
    /// 
    /// May 1st, 2018
    ///     
    /// Business Logic Layer for Surveys
    /// 
    ///</summary>
    public class SurveyTasks
    {
        SurveyDAO surveyDAO = new SurveyDAO();

        public SurveyDO GetSurvey(int id)
        {
            return surveyDAO.GetSurvey(id);
        }
 
        public List<SurveyDO> GetSurveys()
        {
            return surveyDAO.GetSurveys();
        }

        public bool SubmitSurvey(SurveyDO survey)
        {
            return surveyDAO.SubmitSurvey(survey);
        }

        public List<Dictionary<string, string>> GetSurveyResponse(int id)
        {
           return surveyDAO.GetSurveyResponse(id);
        }
    }
}

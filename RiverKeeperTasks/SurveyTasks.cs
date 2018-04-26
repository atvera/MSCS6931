using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDO;
using RiverKeeperDAL;

namespace RiverKeeperTasks
{
    public class SurveyTasks
    {
        SurveyDAO surveyDAO = new SurveyDAO();

        public SurveyDO GetSurvey(int id)
        {
            return surveyDAO.GetSurvey(id);
        }

        public bool SubmitSurvey(SurveyDO survey)
        {
            return surveyDAO.SubmitSurvey(survey);
        }
    }
}

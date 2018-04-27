using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RiverKeeperDO;
using RiverKeeperTasks;
using System.Web.Script.Serialization;

namespace RiverKeeperAPI.Controllers
{
    [Authorize]
    public class SurveyController : ApiController
    {
        SurveyTasks surveyTask = new SurveyTasks();
        
        // GET: api/Survey/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            SurveyDO survey = null;
            try
            {
                survey = surveyTask.GetSurvey(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Json(survey);
        }

        // POST: api/Survey
        [HttpPost]
        public bool Post(SurveyDO survey)
        {
            if(survey.isTemplate)
            {
                return surveyTask.CreateTemplate(survey);
            }
            return surveyTask.SubmitSurvey(survey);
        }

        // PUT: api/Survey/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Survey/5
        public void Delete(int id)
        {
        }
    }
}

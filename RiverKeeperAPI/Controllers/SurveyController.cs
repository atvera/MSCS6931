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
    public class SurveyController : ApiController
    {
        SurveyTasks surveyTask = new SurveyTasks();

        // GET: api/Survey/id
        /// <summary>
        /// Returns the survey with the given id
        /// </summary>
        /// <returns>A JSON object with the stored survey.</returns>
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

        /*
        // GET api/Survey/Template
        /// <summary>
        /// Returns the template survey
        /// </summary>
        /// <returns>A JSON object with the template survey.</returns>
        [Route("api/Survey/Template")]
        [HttpGet] 
        public IHttpActionResult GetTemplate()*/
       

        // POST: api/Survey
        /// <summary>
        /// If the submitted survey is a template, creates a new template
        /// else, submits a survey response
        /// </summary>
        /// <returns>True if transaction was successful</returns>
        [HttpPost]
        public bool Post(SurveyDO survey)
        { 
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

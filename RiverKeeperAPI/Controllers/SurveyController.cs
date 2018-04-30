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
        // GET: api/SurveyResponse/{id}
        /// <summary>
        /// Returns the survey with the given id
        /// </summary>
        /// <returns>A JSON object with the stored survey.</returns>
        [HttpGet]
        [Route("api/SurveyResponse/{id}")]
        public IHttpActionResult GetSurveyResponse(int id)
        {
            List<Dictionary<string,string>> surveyResponse = new List<Dictionary<string, string>>();
            try
            {
               surveyResponse = surveyTask.GetSurveyResponse(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return Json(surveyResponse);
            
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
        /// Submit a survey and save it in the database
        /// </summary>
        /// <returns>True if transaction was successful</returns>
        [HttpPost]
        public bool Post(SurveyDO survey)
        { 
            return surveyTask.SubmitSurvey(survey);
        }

        //// PUT: api/Survey/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Survey/5
        //public void Delete(int id)
        //{
        //}
    }
}

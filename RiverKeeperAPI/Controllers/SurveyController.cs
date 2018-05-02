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
    ///<summary>
    /// Marquette University
    /// MSCS 6931 701-702 Service-Oriented Architecture
    /// Spring 2018 
    /// 
    /// May 1st, 2018
    ///     
    /// API Controller class for Survey
    /// 
    ///</summary>
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
        /// Returns all responses to the survey with the specified id.
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

        // GET: api/AllSurveys
        /// <summary>
        /// Returns all the submitted surveys
        /// </summary>
        /// <returns>A JSON object list of surveys.</returns>
        [Route("api/AllSurveys")]
        [HttpGet]
        public IHttpActionResult AllSurveys()
        {
            List<SurveyDO> survey = null;
            try
            {
                survey = surveyTask.GetSurveys();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Json(survey);
        }


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

        //Future implementation 
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

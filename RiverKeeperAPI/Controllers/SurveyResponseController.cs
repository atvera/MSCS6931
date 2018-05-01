using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using RiverKeeperTasks;

namespace RiverKeeperAPI.Controllers
{

    public class SurveyResponseController : Controller
    {
        // GET: SurveyResponse
        public ActionResult Index()
        {
            SurveyTasks surveytask = new SurveyTasks();
            List<Dictionary<string, string>> answers = surveytask.GetSurveyResponse(2);

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(answers);
            ViewData["Answers"] = json;
        
            return View();
        }
    }
}
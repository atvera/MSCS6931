using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace RiverKeeperAPI.Controllers
{
    ///<summary>
    /// Marquette University
    /// MSCS 6931 701-702 Service-Oriented Architecture
    /// Spring 2018 
    /// 
    /// May 1st, 2018
    ///     
    /// Controller class for the home view
    /// 
    ///</summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Milwaukee Riverkeeper Survey";

            return View();
        }
    }
}

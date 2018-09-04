using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glycano.Controllers
{
    public class Data
    {
        public string Value { get; set; }
    }


    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Post(FormCollection form)
        //{
        // Prepare the process to run
        ProcessStartInfo start = new ProcessStartInfo();
       

//    //var arguments = form["args"];
//    //ViewBag.SessionId = arguments;
//    return View();
//}

[HttpPost]
        public JsonResult GetValue(Data data)
        {
            //Console.WriteLine(start.Arguments);
            //start.FileName = "C:/Users/User/source/repos/Glycano/Glycano/Scripts/CarbBuilder/CarbBuilder2.exe";
            //start.WindowStyle = ProcessWindowStyle.Hidden;
            //start.CreateNoWindow = true;
            //int exitCode;

            //using (Process proc = Process.Start(start))
            //{
            //    proc.WaitForExit();
            //    exitCode = proc.ExitCode;
            //}

            //using (Process proc = Process.Start(start))
            //{
            //    proc.WaitForExit();
            //    exitCode = proc.ExitCode;
            //}

            //string sessionId = System.Web.HttpContext.Current.Session.SessionID;
            return Json(data.Value, JsonRequestBehavior.AllowGet);
            //Data session = new Data();
            //session.Arguments = sessionId;
            //return Json(sessionId, JsonRequestBehavior.AllowGet);
        }
    }
}
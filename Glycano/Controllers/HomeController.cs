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

        [HttpPost]
        public JsonResult GetValue(Data data)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:/Users/dllima001/source/repos/Gly/Glycano/Scripts/CarbBuilder/CarbBuilder2.exe";
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }

            string sessionId = System.Web.HttpContext.Current.Session.SessionID;
            //  return Json(data.Value, JsonRequestBehavior.AllowGet);
            Data session = new Data();
            session.Value = sessionId;
            return Json(session.Value, JsonRequestBehavior.AllowGet);
        }
    }
}
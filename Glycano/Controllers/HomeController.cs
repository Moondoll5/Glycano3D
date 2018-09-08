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
            //To run CarbBuilder
            //Gives path to CarbBuilder and arguments before running it.
            ProcessStartInfo start = new ProcessStartInfo();
            // start.FileName = Server.MapPath("~/CarbBuilder/CarbBuilder2.exe");
            start.FileName = "D:/home/site/wwwroot/CarbBuilder/CarbBuilder2.exe";
            // start.UseShellExecute = false;
            //Gets sessionID unique to current browser.
            string sessionId = System.Web.HttpContext.Current.Session.SessionID;
            Data session = new Data { Value = sessionId };

            //Local host
            //start.Arguments = data.Value + " -o /Users/dllima001/source/repos/Gly/Glycano/Data/" + sessionId;
            start.Arguments = data.Value + " -o /home/site/wwwroot/Data/" + sessionId;
        

            //start.WindowStyle = ProcessWindowStyle.Hidden;
            //start.CreateNoWindow = true;
            int exitCode;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }

            //Sends sessionID to client
            //SessionID is used as filename for PDB files.
            return Json(session.Value, JsonRequestBehavior.AllowGet);
        }
    }
}
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
            start.FileName = Server.MapPath("~/Scripts/CarbBuilder/CarbBuilder2.exe");
           // start.UseShellExecute = false;
            start.ErrorDialog = true;

            //Gets sessionID unique to current browser.
            string sessionId = System.Web.HttpContext.Current.Session.SessionID;
            Data session = new Data();
            session.Value = sessionId;

            start.Arguments = data.Value + " -o " + sessionId;
            Console.WriteLine("heeeeey " + start.Arguments);
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
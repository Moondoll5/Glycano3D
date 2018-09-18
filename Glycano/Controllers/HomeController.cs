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
        public string Repeats { get; set; }
    }


    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GetData(Data data)
        {
            //Checks if CASPER notation was present in the text field. If not, error is returned.
            if (data.Value == null || data.Value == "")
            {
                Data error = new Data { Value = "error" };
                return Json(error.Value, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //To run CarbBuilder. Gives path to CarbBuilder and arguments before running it.
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "D:/home/site/wwwroot/CarbBuilder/CarbBuilder2.exe";

                //Gets sessionID unique to current browser request.
                string sessionId = System.Web.HttpContext.Current.Session.SessionID;
                Data session = new Data { Value = sessionId};

                if (data.Repeats == null || data.Repeats == "")
                {
                    // Creates the arguments to give to CarbBuilder
                    //No repeats were specified
                    start.Arguments = "-i " + data.Value + " -o /home/site/wwwroot/Data/" + sessionId;
                }
                else
                {
                    // Creates the arguments to give to CarbBuilder
                    start.Arguments = "-i " + data.Value + " -r " + data.Repeats + " -o /home/site/wwwroot/Data/" + sessionId;
                }

                int exitCode;

                using (Process proc = Process.Start(start))
                {
                    proc.WaitForExit();
                    exitCode = proc.ExitCode;
                }

                //Sends sessionID to client
                //SessionID is used as a unique filename for PDB files on the server.
                return Json(session.Value, JsonRequestBehavior.AllowGet);
            }
        }

        //private void RunCarbBuilder()
        //{

        //}
    }
}
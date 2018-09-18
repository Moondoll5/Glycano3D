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
                //Gets sessionID unique to current browser request.
                string sessionId = System.Web.HttpContext.Current.Session.SessionID;

                //Creates an object that can be sent back to the client.
                Data session = new Data { Value = sessionId };

                //Runs CarbBuilder
                RunCarbBuilder(data, sessionId);

                //Sends sessionID to client. SessionID is used as a unique filename for the new PDB file.
                return Json(session.Value, JsonRequestBehavior.AllowGet);
            }
        }

        private void RunCarbBuilder(Data data, string sessionId)
        {
            //Input: User-provided arguments and the unique session ID for this request
            //Runs CarbBuilder with user input.

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "D:/home/site/wwwroot/CarbBuilder/CarbBuilder2.exe";

            //Checks if a repeat value was given
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

            //Starts CarbBuilder2.exe
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }
        }
    }
}
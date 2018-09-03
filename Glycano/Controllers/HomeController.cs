using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glycano.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(FormCollection form)
        {
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = form["args"];
            Console.WriteLine(start.Arguments);
            // Enter the executable to run, including the complete path
            start.FileName = "C:/Users/User/source/repos/Glycano/Glycano/Scripts/CarbBuilder/CarbBuilder2.exe";
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            
            int exitCode;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }
            
            //var arguments = form["args"];
            //ViewBag.SessionId = arguments;
            return View();
        }
    }
}
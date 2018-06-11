using ImageServiceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWeb.Controllers
{
    public class LogsController : Controller
    {
        private static LogsModel logsModel = new LogsModel();

        public LogsController()
        {
            
        }

        // GET: Logs
        public ActionResult Logs()
        {
            return View(logsModel);
        }
        
        [HttpPost]
        public ActionResult SelectType(string type)
        {
            if (type == "")
                logsModel.Type = "ALL";
            else
                logsModel.Type = type;
            return RedirectToAction("Logs");
        }
        
    }
}
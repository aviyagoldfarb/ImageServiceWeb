using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageServiceWeb.Models;
using System.Threading;

namespace ImageServiceWeb.Controllers
{
    public class ConfigController : Controller
    {
        private static ConfigModel configModel = new ConfigModel();
        private static string handlerToRemove; 

        public ConfigController()
        {
            Thread.Sleep(1000);
            //configModel = new ConfigModel();
        }

        public ActionResult Config()
        {
            return View(configModel);
        }
        
        public ActionResult DeleteHandler(string handlerPath)
        {
            handlerToRemove = handlerPath;
            return View("RemoveHandler");
        }

        [HttpGet]
        public ActionResult SendRequestForRemovingHandler()
        {
            configModel.RemoveHandler(handlerToRemove);
            return RedirectToAction("Config");
        }

        [HttpGet]
        public ActionResult Cancel()
        {
            return RedirectToAction("Config");
        }

    }
}

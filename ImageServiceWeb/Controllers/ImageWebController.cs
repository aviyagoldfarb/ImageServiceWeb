using ImageServiceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWeb.Controllers
{
    public class ImageWebController : Controller
    {
        private static ImageWebModel imageWebModel = new ImageWebModel();

        public ImageWebController()
        {
            //imageWebModel = new ImageWebModel();
        }

        // GET: ImageWeb
        public ActionResult ImageWeb()
        {
            imageWebModel.GetPhotosNum();
            return View(imageWebModel);
        }
    }
}
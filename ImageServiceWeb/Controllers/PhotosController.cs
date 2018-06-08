using ImageServiceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWeb.Controllers
{
    public class PhotosController : Controller
    {
        private PhotosModel photosModel;

        public PhotosController()
        {
            photosModel = new PhotosModel();
        }

        // GET: Photos
        public ActionResult Photos()
        {
            return View(photosModel);
        }
    }
}
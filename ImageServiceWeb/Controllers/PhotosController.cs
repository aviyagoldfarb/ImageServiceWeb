using ImageServiceWeb.Infrastructure;
using ImageServiceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWeb.Controllers
{
    public class PhotosController : Controller
    {
        private static PhotosModel photosModel = new PhotosModel();

        public PhotosController()
        {
            //photosModel = new PhotosModel();
        }

        // GET: Photos
        public ActionResult Photos()
        {
            photosModel.GetPhotos();
            return View(photosModel);
        }

        //[HttpGet]
        public ActionResult ViewPhoto(string thumbnailPath, string fullSizePath, string relativeThumbnailPath, string relativePath, string photoName, string photoDate)
        {
            ViewBag.ThumbnailPath = thumbnailPath;
            ViewBag.FullSizePath = fullSizePath;
            ViewBag.RelativeThumbnailPath = relativeThumbnailPath;
            ViewBag.RelativePath = relativePath;
            ViewBag.PhotoName = photoName;
            ViewBag.PhotoDate = photoDate;
            return View();
        }

        public ActionResult DeletePhotoConfirm(string thumbnailPath, string fullSizePath, string relativeThumbnailPath, string relativePath, string photoName, string photoDate)
        {
            ViewBag.ThumbnailPath = thumbnailPath;
            ViewBag.FullSizePath = fullSizePath;
            ViewBag.RelativeThumbnailPath = relativeThumbnailPath;
            ViewBag.RelativePath = relativePath;
            ViewBag.PhotoName = photoName;
            ViewBag.PhotoDate = photoDate;
            return View();
        }

        public ActionResult DeletePhoto(string fullSizePath, string thumbnailPath)
        {
            photosModel.DeletePhoto(fullSizePath, thumbnailPath);
            photosModel.GetPhotos();
            return RedirectToAction("Photos");
        }

    }
}
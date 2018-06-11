using ImageServiceWeb.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Models
{
    public class PhotosModel
    {
        private OutputDirModel outputDirModel;

        [Required]
        [Display(Name = "Output Dir: ")]
        public string OutputDir { get; set; }

        [Required]
        [Display(Name = "PhotosList")]
        public List<PhotoInfo> PhotosInformation { get; set; }

        /// <summary>
        /// constructor.
        /// </summary>
        public PhotosModel()
        {
            this.outputDirModel = OutputDirModel.Instance;
            OutputDir = this.outputDirModel.OutputDir;
            PhotosInformation = new List<PhotoInfo>();
        }

        public void GetPhotos()
        {
            PhotosInformation.Clear();
            string[] extensions = { ".jpg", ".png", ".gif", ".bmp" };
            try
            {
                string[] filesPathsList = Directory.GetFiles(OutputDir + "\\Thumbnails", "*", SearchOption.AllDirectories);
                foreach (string filePath in filesPathsList)
                {
                    if (extensions.Contains(Path.GetExtension(filePath.ToLower())))
                    {
                        PhotosInformation.Add(new PhotoInfo(filePath));
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void DeletePhoto(string fullSizePath, string thumbnailPath)
        {
            File.Delete(fullSizePath);
            File.Delete(thumbnailPath);
        }

    }
}
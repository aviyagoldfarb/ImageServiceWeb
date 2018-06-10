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

            string[] thumbnailPhotosPaths = Directory.GetFiles(OutputDir + "\\Thumbnails", "*", SearchOption.AllDirectories);

            PhotosInformation = new List<PhotoInfo>();

            foreach (string photoPath in thumbnailPhotosPaths)
            {
                PhotosInformation.Add(new PhotoInfo(photoPath));
            }
        }
        
    }
}
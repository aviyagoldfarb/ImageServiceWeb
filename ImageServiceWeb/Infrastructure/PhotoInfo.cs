using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Infrastructure
{
    public class PhotoInfo
    {
        public string ThumbnailPath { get; private set; }
        public string FullSizePath { get; private set; }
        public string RelativePath { get; private set; }
        public string PhotoName { get; private set; }
        public string PhotoDate { get; private set; }

        public PhotoInfo(string path)
        {
            this.ThumbnailPath = path;
            string[] infoFromPath = InfoFromPath(path);

            this.FullSizePath = infoFromPath[0];
            this.RelativePath = infoFromPath[1];
            this.PhotoName = infoFromPath[2];
            this.PhotoDate = infoFromPath[3];
        }

        private string[] InfoFromPath(string path)
        {
            string[] infoFromPath = new string[4];

            string[] splitByThumbnail = path.Split(new string[] { "\\Thumbnails" }, StringSplitOptions.None);
            string fullSizePath = splitByThumbnail[0] + splitByThumbnail[1];
            infoFromPath[0] = fullSizePath;

            string[] splitByImageServiceWeb = path.Split(new string[] { "ImageServiceWeb" }, StringSplitOptions.None);
            infoFromPath[1] = splitByImageServiceWeb[2];

            string fileName = Path.GetFileNameWithoutExtension(path);
            infoFromPath[2] = fileName;

            string[] dateCreator = splitByThumbnail[1].Split('\\');
            string fileDate = dateCreator[1] + "/" + dateCreator[2];
            infoFromPath[3] = fileDate;

            

            //infoFromPath[1] = "/App_Data/OutputDir/Thumbnails/" + fileDate + "/" + Path.GetFileName(path);

            return infoFromPath;
        }
    }
}
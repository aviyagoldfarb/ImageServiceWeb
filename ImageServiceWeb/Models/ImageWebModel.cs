using ImageServiceWeb.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;
using ImageServiceWeb.Infrastructure;

namespace ImageServiceWeb.Models
{
    public class ImageWebModel
    {
        private ITcpClient tcpClient;

        [Required]
        [Display(Name = "Is Connected")]
        public bool IsConnected { get; set; }

        [Required]
        [Display(Name = "Photos Number")]
        public string PhotosNum { get; set; }

        [Required]
        [Display(Name = "StudentsInformation")]
        public List<StudentsInfo> StudentsInformation { get; set; }

        /// <summary>
        /// constructor.
        /// </summary>
        public ImageWebModel()
        {
            this.tcpClient = ServiceTcpClient.Instance;
            IsConnected = this.tcpClient.Connected();

            int filesCounter = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/App_Data/OutputDir"), "*", SearchOption.AllDirectories).Length;
            PhotosNum = (filesCounter / 2).ToString();

            StudentsInformation = new List<StudentsInfo>();
            string[] studentsInfoLines = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/App_Data/StudentsInfo.txt"));
            foreach (string line in studentsInfoLines)
            {
                string[] info = line.Split(' ');
                StudentsInformation.Add(new StudentsInfo(info[0], info[1], info[2]));
            }
        }
    }
}
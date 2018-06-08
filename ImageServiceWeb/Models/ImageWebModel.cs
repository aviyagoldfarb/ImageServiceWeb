using ImageServiceWeb.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ImageServiceWeb.Models
{
    public class ImageWebModel
    {
        private ITcpClient tcpClient;

        [Required]
        [Display(Name = "Is Connected")]
        public bool IsConnected { get; set; }

        /// <summary>
        /// constructor.
        /// </summary>
        public ImageWebModel()
        {
            this.tcpClient = ServiceTcpClient.Instance;
            IsConnected = this.tcpClient.Connected();
        }
    }
}
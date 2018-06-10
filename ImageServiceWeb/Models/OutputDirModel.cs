using ImageServiceWeb.Communication;
using ImageServiceWeb.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;

namespace ImageServiceWeb.Models
{
    public class OutputDirModel
    {
        private ITcpClient tcpClient;
        private volatile Boolean stop;
        
        [Required]
        [Display(Name = "Output Dir: ")]
        public string OutputDir { get; set; }

        private OutputDirModel()
        {
            this.tcpClient = ServiceTcpClient.Instance;
            this.stop = false;
            this.tcpClient.ConfigRecieved += OnConfigRecieved;
            this.Start();
        }

        private static OutputDirModel instance;
        /// <summary>
        /// creating an instance from this class in the first time, and return this object. 
        /// </summary>
        public static OutputDirModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OutputDirModel();
                }
                Thread.Sleep(1000);
                return instance;
            }
        }

        /*
        /// <summary>
        /// constructor.
        /// </summary>
        public OutputDirModel()
        {
            this.tcpClient = ServiceTcpClient.Instance;
            this.stop = false;
            this.tcpClient.ConfigRecieved += OnConfigRecieved;
            this.Start();
        }
        */

        /// <summary>
        /// closing the connection with the service.
        /// </summary>
        public void Disconnect()
        {
            stop = true;
            tcpClient.Disconnect();
        }

        /// <summary>
        /// starting to get settings from the service.
        /// </summary>
        public void Start()
        {
            tcpClient.Write("GetConfigCommand");
        }

        /// <summary>
        /// receiving a configuration.
        /// </summary>
        /// <param name="sender">the sender of the massage</param>
        /// <param name="msg">the configurations that reseived </param>
        public void OnConfigRecieved(object sender, MessageEventArgs msg)
        {
            // Parsing the message
            string message = msg.Message;
            JObject obj = JObject.Parse(message);
            // Updating the configuration field
            OutputDir = obj["OutputDir"].ToString();
        }

        public ITcpClient GetTcpClient()
        {
            return this.tcpClient;
        }
    }
}
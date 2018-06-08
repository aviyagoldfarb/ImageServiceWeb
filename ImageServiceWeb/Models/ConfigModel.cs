using ImageServiceWeb.Communication;
using ImageServiceWeb.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ImageServiceWeb.Models
{
    public class ConfigModel
    {
        
        private ITcpClient tcpClient;
        private volatile Boolean stop;

        [Required]
        [Display(Name = "Handlers")]
        public List<string> Handlers { get; set; }
        
        [Required]
        [Display(Name = "Output Dir: ")]
        public string OutputDir { get; set; }

        [Required]
        [Display(Name = "Source Name: ")]
        public string SourceName { get; set; }

        [Required]
        [Display(Name = "Log Name: ")]
        public string LogName { get; set; }

        [Required]
        [Display(Name = "Thumbnail Size: ")]
        public int ThumbnailSize { get; set; }

        /// <summary>
        /// constructor.
        /// </summary>
        public ConfigModel()
        {
            this.tcpClient = ServiceTcpClient.Instance;
            this.stop = false;
            this.tcpClient.ConfigRecieved += OnConfigRecieved;
            this.tcpClient.RemovedHandler += OnRemovedHandler;
            //handlers = new ObservableCollection<string>();
            Handlers = new List<string>();
            this.Start();
        }
        
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
        /// sending a command to remove a handler.
        /// </summary>
        /// <param name="handlerPath"> the path of the directory that handling by the handler that should to be remove</param>
        public void RemoveHandler(string handlerPath)
        {
            new Thread(delegate () {

                try
                {
                    tcpClient.Write("RemoveHandler" + " " + handlerPath/*this.SelectedHandler*/);
                }
                catch (Exception)
                {

                }
            }).Start();
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
            SourceName = obj["SourceName"].ToString();
            LogName = obj["LogName"].ToString();
            int.TryParse(obj["ThumbnailSize"].ToString(), out int thumbnailSize);
            ThumbnailSize = thumbnailSize;
            // Parsing the handlers Paths
            string[] handlerPaths = JsonConvert.DeserializeObject<string[]>(obj["HandlersPaths"].ToString());
            // Adding each handler to the Handlers list
            foreach (string path in handlerPaths)
            {
                Handlers.Add(path);
            }
        }

        /// <summary>
        /// get and send a message that one handler is removed.
        /// </summary>
        /// <param name="sender">the sender.</param>
        /// <param name="msg">the path of the handler.</param>
        public void OnRemovedHandler(object sender, MessageEventArgs msg)
        {
            // Parsing the message
            string message = msg.Message;
            JObject obj = JObject.Parse(message);
            string path = obj["RemovedHandlerPath"].ToString();

            if (this.Handlers.Contains(path))
            {
                this.Handlers.Remove(path);
            }
        }
        
    }
}

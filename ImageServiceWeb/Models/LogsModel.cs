using ImageServiceWeb.Communication;
using ImageServiceWeb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImageServiceWeb.Models
{
    public class LogsModel
    {
        private ITcpClient tcpClient;
        private volatile Boolean stop;
        private static int logMapIndex = 1;

        [Required]
        [Display(Name = "Logs")]
        public List<LogMessage> Logs { get; set; }
        
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }
        /// <summary>
        /// constructor.
        /// </summary>
        public LogsModel()
        {
            this.tcpClient = ServiceTcpClient.Instance;
            this.stop = false;
            this.tcpClient.LogUdated += OnLogUpdated;
            Logs = new List<LogMessage>();
            Type = "ALL";
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
        /// starting to get logs from the service.
        /// </summary>
        public void Start()
        {
            tcpClient.Write("LogCommand");
        }

        /// <summary>
        /// updating with the messages that reseived from the servise.
        /// </summary>
        /// <param name="sender">the sender of the message</param>
        /// <param name="msg">the message to log</param>
        public void OnLogUpdated(object sender, MessageEventArgs msg)
        {
            string log = msg.Message;
            JObject obj = JObject.Parse(log);
            Dictionary<int, string[]> map = new Dictionary<int, string[]>
                    (JsonConvert.DeserializeObject<Dictionary<int, string[]>>(obj["LogMap"].ToString()));
            int size = map.Count;
            if (size > 1)
            {
                for (int i = 1; i <= size; i++, logMapIndex++)
                {
                    string[] str = map[i];
                    string type = MessageTypeConverter(str[0]);
                    string message = str[1];
                    LogMessage logMessage = new LogMessage(type, message);
                    Logs.Add(logMessage);
                }
            }
            else if(size == 1)
            {
                string[] str = map[logMapIndex];
                logMapIndex++;
                string type = MessageTypeConverter(str[0]);
                string message = str[1];
                LogMessage logMessage = new LogMessage(type, message);
                Logs.Add(logMessage);
            }
            
        }

        /// <summary>
        /// The method converts from the message Type to the required one.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string MessageTypeConverter(string msg)
        {
            switch (msg)
            {
                case "Information":
                    return "INFO";
                    
                case "Warning":
                    return "WARNING";
                    
                case "FailureAudit":
                    return "FAIL";
                    
                default:
                    return null;
                    
            } 
        }
        
    }
}

﻿using ImageServiceWeb.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageServiceWeb.Communication
{
    public class ServiceTcpClient : ITcpClient
    {
        //private IPEndPoint ep;
        private TcpClient client;
        //private NetworkStream stream;

        public event EventHandler<MessageEventArgs> ConfigRecieved;
        public event EventHandler<MessageEventArgs> LogUdated;
        public event EventHandler<MessageEventArgs> RemovedHandler;
        
        /// <summary>
        /// creating a connection to the server of service
        /// </summary>
        private ServiceTcpClient()
        {
            this.client = new TcpClient();
            
            try
            {
                this.client.Connect("127.0.0.1", 8000);
            }
            catch (SocketException)
            {

            }
            this.Read();
            //Thread.Sleep(100);
        }

        private static ServiceTcpClient instance;
        /// <summary>
        /// creating an instance from this class in the first time, and return this object. 
        /// </summary>
        public static ServiceTcpClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceTcpClient();
                }
                //Thread.Sleep(1000);
                return instance;
            }
        }

        /// <summary>
        /// writing a command to the service
        /// </summary>
        /// <param name="command">the command that should to be sent</param>
        public void Write(string command)
        {
            
            if (!this.client.Connected)
            {
                return;
            }
            new Task(() =>
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    {
                        // Send data to server
                        writer.Write(command);
                        writer.Flush();
                    }
                }
                catch (Exception)
                {

                }
            }).Start();
        }

        /// <summary>
        /// reading a message from the service
        /// </summary>
        public void Read()
        {
            new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                {
                    while (true)
                    {
                        try
                        {
                            // Get result from server
                            string result = reader.ReadString();
                            ProcessAndSend(result);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                        
                    }
                }
            }).Start();
        }

        /// <summary>
        /// return true if the client is connected and false otherwise.
        /// </summary>
        /// <returns>true if the client is connected and false otherwise.</returns>
        public bool Connected()
        {
            return this.client.Connected;
        }

        /// <summary>
        /// closing the connection.
        /// </summary>
        public void Disconnect()
        {
            client.Close();
        }

        /// <summary>
        /// reading the message from the service and invoke the relevant listeners about this message.
        /// </summary>
        /// <param name="message">the message that received from the service.</param>
        private void ProcessAndSend(string message)
        {
            // Parsing our jobjects command enum
            JObject obj = JObject.Parse(message);
            int.TryParse(obj["CommandEnum"].ToString(), out int command);
            MessageEventArgs m = new MessageEventArgs(message);
            // Sending the server message to the right gui tab via event
            switch (command)
            {
                case (int)Infrastructure.Enums.CommandEnum.GetConfigCommand:
                    ConfigRecieved?.Invoke(this, m);
                    break;
                case (int)Infrastructure.Enums.CommandEnum.LogCommand:
                    LogUdated?.Invoke(this, m);
                    break;
                case (int)Infrastructure.Enums.CommandEnum.CloseCommand:
                    RemovedHandler?.Invoke(this, m);
                    break;
                default:
                    break;
            }
        }
    }
}

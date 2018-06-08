using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceWeb.Infrastructure
{
    
    public class MessageEventArgs : EventArgs
    {
        //public int CommandID { get; set; }
        public string Message { get; set; }

        public MessageEventArgs(/*int id,*/ string msg)
        {
            //CommandID = id;
            Message = msg;
        }
    }
}

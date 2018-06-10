using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Infrastructure
{
    public class StudentsInfo
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ID { get; private set; }

        public StudentsInfo(string firstName, string lastName, string id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ID = id;
        }
    }
}
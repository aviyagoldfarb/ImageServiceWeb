using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Infrastructure
{
    public class StudentsInfo
    {
        public string firstName;
        public string lastName;
        public string id;

        public StudentsInfo(string firstName, string lastName, string id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = id;
        }
    }
}
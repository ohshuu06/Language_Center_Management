using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Language_Center_Management.Models
{
    public class Student
    {
        public string Student_ID { get; set; }
        public string Student_Name { get; set; }
        public string Student_Phone { get; set; }
        public string Student_Email { get; set; }
        public DateTime Student_DOB { get; set; }
        public string Register_Language { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
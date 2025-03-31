using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Language_Center_Management.Models
{
    public class Grade
    {
        public string Assignment_Code { get; set; }
        public string Student_ID { get; set; }
        public string Course_ID { get; set; }
        public DateTime Assignment_Date { get; set; }
        public decimal? Assignment_Grade { get; set; }
    }
}
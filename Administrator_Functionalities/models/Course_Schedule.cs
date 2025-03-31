using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrator_Functionalities.Models
{
    public class Course_Schedule
    {
        public string courseID { get; set;}
        public DateTime startingDate { get; set;}
        public DateTime endingDate { get; set;}
        public string roomNo { get; set;}
    }
}

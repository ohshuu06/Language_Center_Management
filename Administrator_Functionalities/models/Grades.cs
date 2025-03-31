using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Administrator_Functionalities.Models
{
    public class Grades
    {
        public string assignmentCode { get; set;}
        public string courseID { get; set;}
        public string studentID { get; set;}
        public DateTime assignmentDate { get; set;}
        public decimal grade { get; set;}
    }
}

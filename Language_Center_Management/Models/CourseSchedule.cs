using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Language_Center_Management.Models
{
    public class CourseSchedule
    {
        public string Course_ID { get; set; }
        public string Course_name{ get; set; }
        public string Language_Name { get; set; }
        public DateTime Starting_Date { get; set; }
        public DateTime Ending_Date { get; set; }
        public string DayOfWeek { get; set; }
        public string Starting_Time { get; set; }
        public string Ending_Time { get; set; }
        public string Room_No { get; set; }
        public virtual Course Course { get; set; }
        public virtual Classroom Classroom { get; set; }

    }
}
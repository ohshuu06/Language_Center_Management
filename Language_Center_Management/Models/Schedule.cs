using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Language_Center_Management.Models
{
    public class Schedule
    {
        public string Course_ID { get; set; }
        public string DayofWeek { get; set; }
        public string Starting_Time { get; set; }

        // Liên kết với khóa học
        public virtual Course Course { get; set; }
    }
}
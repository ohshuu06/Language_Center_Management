using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrator_Functionalities.Models
{
    public class Schedule
    {
        public string courseID { get; set;}
        public string dayofWeek { get; set;}
        public TimeSpan startingTime { get; set;}
    }
}

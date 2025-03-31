using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrator_Functionalities.Models
{
    public class Schedule_Time
    {
        public string dayofWeek { get; set;}
        public TimeSpan startingTime { get; set;}
        public TimeSpan endingTime { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityMonitor.Dto
{
    public class Activity
    {
        public string activeWindowTitle { get; set; }
        public DateTime activatedWindowDateTime { get; set; }
        public DateTime windowActiveEndDateTime { get; set; }
        public TimeSpan windowActiveTimeSpan { get; set; }
    }
}

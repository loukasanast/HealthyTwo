using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public interface IActivity
    {
        TimeSpan Period { get; set; }
        int TotalDistance { get; set; }
        int Speed { get; set; }
        int AverageHeartRate { get; set; }
        int HoleNumber { get; set; }
        int Duration { get; set; }
        DateTime StartTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class SleepActivity : IActivity
    {
        public TimeSpan Period { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public int AverageHeartRate { get; set; }

        public int HoleNumber
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int Speed
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int TotalDistance
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class GuidedWorkoutActivity : IActivity
    {
        public int AverageHeartRate { get; set; }

        public int Duration
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int HoleNumber
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public TimeSpan Period { get; set; }
        public int Speed { get; set; }

        public DateTime StartTime
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int TotalDistance { get; set; }
    }
}

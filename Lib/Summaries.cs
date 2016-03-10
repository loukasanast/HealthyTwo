using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Summaries : IActivity
    {
        public TimeSpan Period { get; set; }
        public int TotalDistance { get; set; }
        public int Speed { get; set; }
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

        public DateTime StartTime
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}

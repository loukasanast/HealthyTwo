﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class GolfActivity : IActivity
    {
        public TimeSpan Period { get; set; }
        public int TotalDistance { get; set; }
        public int HoleNumber { get; set; }
        public int AverageHeartRate { get; set; }

        public int Duration
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int Speed
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

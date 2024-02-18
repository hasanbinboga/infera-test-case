using System;

namespace Infera.TestCase
{
    public class Date
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public DateTime? DateTime 
        { 
            get{ return new DateTime(Year, Month, Day); } 
        }
    }
}

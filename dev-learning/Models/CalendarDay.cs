using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dev_learning.Models
{
    public class CalendarDay
    {
        public CalendarDay(int day, Subject subject, bool isLearned)
        {
            Day = day;
            Subject = subject;
            IsLearned = isLearned;
        }
        public int Day { get; set; }
        public Subject Subject { get; set; }
        public bool IsLearned { get; set; }
    }
}

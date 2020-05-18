using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dev_learning.Models
{
    public class CalendarDay
    {
        public CalendarDay(int day)
        {
            Day = day;
            Subjects = new List<(bool, Subject)>();
        }
        public int Day { get; set; }
        public List<(bool, Subject)> Subjects { get; set; }

        public void AddSubject(Subject subject, bool isLearned)
        {
            Subjects.Add((isLearned, subject));
        }
    }
}

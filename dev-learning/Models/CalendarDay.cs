using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dev_learning.Models
{
    public class CalendarDay
    {
        public CalendarDay(int day, List<CustomUserSubject> userSubjects)
        {
            Day = day;
            UserSubjects = userSubjects;
        }
        public int Day { get; set; }
        public List<CustomUserSubject> UserSubjects { get; set; }

    }
}

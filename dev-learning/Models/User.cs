using Newtonsoft.Json;
using System.Collections.Generic;

namespace dev_learning.Models
{
    public enum UserRole
    {
        Junior,
        Mid,
        Senior,
        TeamLead,
        God,
    }
    public class User
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public int LearningDaysLeft { get; set; }
        public virtual ICollection<UserSubject> Subjects { get; set; }

    }
}


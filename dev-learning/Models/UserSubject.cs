

using System.ComponentModel.DataAnnotations;

namespace dev_learning.Models
{
    public class UserSubject
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int DaysLearned { get; set; }
    }
}

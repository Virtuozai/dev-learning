using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dev_learning.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? SubjectId { get; set; }
        public int? UserSubjectId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

    }
}

using System;

namespace dev_learning.Models
{
   
    public class Comment
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public string Text { get; set; }
        public DateTime Datetime { get; set; }

    }
}

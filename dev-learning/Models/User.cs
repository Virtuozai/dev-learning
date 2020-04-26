﻿using System.Collections.Generic;

namespace dev_learning.Models
{
   
    public class User
    {

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int LearningDaysLeft { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}


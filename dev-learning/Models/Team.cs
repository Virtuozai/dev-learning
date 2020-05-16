using System;
using System.Collections.Generic;
using System.Text;

namespace dev_learning.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? TeamLeadId { get; set; }
        public User TeamLead { get; set; }

    }
}

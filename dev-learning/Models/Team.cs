using System;
using System.Collections.Generic;
using System.Text;

namespace dev_learning.Models
{
    public class Team
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public int TeamLeadId { get; set; }
    }
}

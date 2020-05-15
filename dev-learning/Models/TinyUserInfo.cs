using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dev_learning.Models
{
    public class TinyUserInfo
    {
        public TinyUserInfo(string email, string role, string id)
        {
            Id = id;
            Email = email;
            Role = role;
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}

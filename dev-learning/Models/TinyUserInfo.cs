using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dev_learning.Models
{
    public class TinyUserInfo
    {
        public TinyUserInfo(string fullName, string email, string role, string id)
        {
            Id = id;
            Email = email;
            Fullname = fullName;
            Role = role;
        }
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}

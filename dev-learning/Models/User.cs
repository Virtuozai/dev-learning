using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

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
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [IgnoreDataMember]
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public int LearningDaysLeft { get; set; }


        public User(int id, int? teamId, string firstName, string lastName, string email, string password, UserRole role, int learningDaysLeft)
        {
            Id = id;
            TeamId = teamId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
            LearningDaysLeft = learningDaysLeft;
        }
    }

    public class NewUser
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public int LearningDaysLeft { get; set; }

    }
}



using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dev_learning.Models
{
    public class CustomUserSubject
    {
        public int Id { get; set; }
        public Subject Subject { get; set; }
        public bool IsLearned { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}

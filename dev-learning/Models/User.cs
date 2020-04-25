namespace dev_learning.Models
{
   
    public class User
    {

        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public int learningDaysLeft { get; set; }

    }
}


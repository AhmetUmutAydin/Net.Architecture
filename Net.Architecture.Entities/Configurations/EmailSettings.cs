using System.Net.Mail;

namespace Net.Architecture.Entities.Configurations
{
    public class EmailSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHtml { get; set; }
        public MailPriority Priority { get; set; }
        public string FromMailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

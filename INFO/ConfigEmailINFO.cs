using System.Net;
using System.Net.Mail;

namespace INFO
{
    public class ConfigEmailINFO
    {
        public string Host { get; set; }
        public bool EnableSsl { get; set; }
        public NetworkCredential Credentials { get; set; }
        public MailPriority Prioridade { get; set; }
    }
}


using BarbaraDoces.Models;

namespace DAL
{
    public class DALEmail
    {
        public ConfigEmailViewModel busRetornaDadosConfigEmail()
        {
            ConfigEmailViewModel objConfigEmail;

            objConfigEmail = new ConfigEmailViewModel()
            {
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("ed.junior646@gmail.com", "amaterasu_2016"),
                Prioridade = System.Net.Mail.MailPriority.High
            };

            return objConfigEmail;
        }
    }
}

using System;
using System.Net;
using System.Net.Mail;
using INFO;
using DAL;

namespace BLL
{
   public class BLLEmail
    {
        //Método de envio de e-mail
        public bool busEnviarEmail(EmailINFO objEmail)
        {
            DALEmail DALEmail;
            SmtpClient client;
            MailMessage mail;
            ConfigEmailINFO objConfigEmail;

            try
            {
                DALEmail = new DALEmail();
                objConfigEmail = new ConfigEmailINFO();

                //Retorna os dados de configuração do email
                objConfigEmail = DALEmail.busRetornaDadosConfigEmail();

                //Populando o objeto SmtpClient com os dados do servidor e credenciais do servidor de email
                client = new SmtpClient()
                {
                    Host = objConfigEmail.Host,
                    EnableSsl = objConfigEmail.EnableSsl,
                    Credentials = objConfigEmail.Credentials
                };

                //Populando o objeto MailMessenger com os dados do email
                mail = new MailMessage()
                {
                    Sender = new MailAddress(objEmail.Email, objEmail.Nome), //Remetente
                    From = new MailAddress(objEmail.Email, objEmail.Nome), //Remetente
                    Subject = "Contato",
                    Priority = objConfigEmail.Prioridade,
                    IsBodyHtml = true,
                    Body =
                    "Mensagem do site:" +
                    "<br/> Nome:  " + objEmail.Nome +
                    "<br/> Email : " + objEmail.Email +
                    "<br/> Telefone : " + objEmail.Telefone +
                    "<br/> Assunto : " + objEmail.Assunto +
                    "<br/> Mensagem : " + objEmail.Mensagem
                };

                mail.To.Add(new MailAddress("edson.bjunior@valid.com", "Edson Junior")); //Destinatário

                client.Send(mail);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objConfigEmail = null;
                DALEmail = null;
                client = null;
                mail = null;
            }
        }
    }
}

using System.Net;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace BarbaraDoces.Models
{
    public class EmailViewModel
    {
        [Required]
        [Display(Name = "Nome: ")]
        [StringLength(75, ErrorMessage = "O nome deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Endereço de Email: ")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Número de telefone")]
        public string Number { get; set; }

        [Display(Name = "Assunto: ")]
        public string Assunto { get; set; }

        [Required]
        [Display(Name = "Mensagem: ")]
        [StringLength(255, ErrorMessage = "A mensagem deve ter no mínimo {10} caracteres.", MinimumLength = 10)]
        public string Mensagem { get; set; }
    }

    public class ConfigEmailViewModel
    {
        [Display(Name = "Host do servidor de e-mail: ")]
        public string Host { get; set; }
        [Display(Name = "Ativar SSL? ")]
        public bool EnableSsl { get; set; }
        [Display(Name = "Credenciais para envio: ")]
        public NetworkCredential Credentials { get; set; }
        [Display(Name = "Prioridade de leitura: ")]
        public MailPriority Prioridade { get; set; }
    }
}
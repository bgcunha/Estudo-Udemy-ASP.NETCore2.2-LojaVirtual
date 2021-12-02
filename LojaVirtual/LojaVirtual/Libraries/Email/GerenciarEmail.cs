
using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace LojaVirtual.Libraries
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp { get; set; }
        private IConfiguration _configuration { get; set; }
        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }

        public void EnviarContatoPorEmail(Contato contato)
        {

            var corpoEmail = $"<h2>Contato - Loja Virtual<h2/> <br/>" +
                $"<b>Nome: <b/> {contato.Nome}<br/>" +
                $"<b>Email: <b/> {contato.Email}<br/>" +
                $"<b>Texto:<b/> {contato.Texto}<br/>" +
                $"<br/> E-mail enviado automaticamente do site Loja Virtual";

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_configuration.GetValue<string>("Email:Username")),
                Subject = $"Contato - Loja Virtual E-mail: {contato.Email}",
                Body = corpoEmail,
                IsBodyHtml = true,
            };

            mail.To.Add("brunnu.cunha@gmail.com");

            _smtp.Send(mail);
        }

        public void EnviarSenhaColaborador(Colaborador colaborador)
        {
            var corpoEmail = $"<h2>Colaborador - Loja Virtual<h2/> <br/>" +
                             $"Sua senha é " +
                             $"<h3>{colaborador.Senha}</h3>" +
                             $"<br/> E-mail enviado automaticamente do site Loja Virtual";

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_configuration.GetValue<string>("Email:Username")),
                Subject = $"Colaborador - Loja Virtual Senha do colaborador {colaborador.Nome}",
                Body = corpoEmail,
                IsBodyHtml = true,
            };

            mail.To.Add(colaborador.Email);

            _smtp.Send(mail);
        }
    }
}

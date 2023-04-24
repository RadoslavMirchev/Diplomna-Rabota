using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Plugins;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Beauty_Salon.Models;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;

namespace Beauty_Salon.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = _configuration["EmailSettings:SmtpPort"];
            var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
            var smtpPassword = _configuration["EmailSettings:SmtpPassword"];

            var sendemail = new MailMessage(smtpUsername, email, subject, message);

            using (var client = new SmtpClient(smtpServer, int.Parse(smtpPort))
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword)
            })
            {
                await client.SendMailAsync(sendemail);
            }
        }
        /*public async Task SendConfirmationEmailAsync(string email, string subject, string message, string code)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = _configuration["EmailSettings:SmtpPort"];
            var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
            var smtpPassword = _configuration["EmailSettings:SmtpPassword"];

            var thing = new MailMessage(smtpUsername, email, subject, message);

            var client = new SmtpClient(smtpServer, int.Parse(smtpPort))
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword)
            };
            {
                await client.SendMailAsync(thing);
            }
        }   */
    }
}

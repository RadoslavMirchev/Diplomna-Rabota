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

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("beautysalon934@gmail.com", "bbygcaihmzutkrjk")
            };
            await client.SendMailAsync("beautysalon934@gmail.com", email, subject, message);
        }
    }
}

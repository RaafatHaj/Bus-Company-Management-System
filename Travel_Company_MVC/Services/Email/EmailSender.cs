using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using TravelCompany.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using TravelCompany.Domain.Eums;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using Travel_Company_MVC.Settings;
using System.Data;
using Microsoft.Extensions.Options;
using System.Net;


namespace Travel_Company_MVC.Services.Email
{
    public class EmailSender : IEmailSender
    {

        
        private readonly MailSettings _mailSettings;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public EmailSender( 
            IOptions<MailSettings> mailSettings,
            IWebHostEnvironment webHostEnvironment)
        {
         
            _mailSettings = mailSettings.Value;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {


            MailMessage message = new()
            {
                From = new MailAddress(_mailSettings.Email!, _mailSettings.DisplayName),
                Body = htmlMessage,
                Subject = subject,
                IsBodyHtml = true
            };

            message.To.Add(_webHostEnvironment.IsDevelopment() ? "omer.cec57@gmail.com" : email);


            using (SmtpClient smtpClient = new(_mailSettings.Host)
            {
                Port = _mailSettings.Port,
                Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password),
                EnableSsl = true
            })
            {

                await smtpClient.SendMailAsync(message);
            }


        }




    }
}

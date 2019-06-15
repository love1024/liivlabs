﻿using liivlabs_shared.Interfaces.SMTP;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Email Sender External Service
/// </summary>
namespace liivlabs_infrastructure.SMTP
{
    public class EmailSender: IEmailSender
    {
        private string sendGridSecret;

        /// <summary>
        /// Inject Services
        /// </summary>
        public EmailSender(IConfiguration configuration)
        {
            this.sendGridSecret = configuration["SENDGRID_KEY"];
        }

        /// <summary>
        /// Send Verification Email to given User
        /// </summary>
        /// <param name="emailTo"></param>
        /// <returns></returns>
        public async Task SendEmailForVerification(string emailTo, string token)
        {
            //Get email template and insert email and token
            string subject = "EZNOTES: Email Verification";
            string emailContent = System.IO.File.ReadAllText(@"./template/emailverify.txt");
            emailContent = emailContent.Replace("{{EMAIL}}", emailTo).Replace("{{CODE}}",token);

            SendGridClient client = new SendGridClient(this.sendGridSecret);
            EmailAddress from = new EmailAddress("lsk18mto@gmail.com");
            EmailAddress to = new EmailAddress(emailTo);
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject,"", emailContent);

            await client.SendEmailAsync(msg);
        }

        /// <summary>
        /// Send password reset link to given user
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task  SendPasswordResetEmail(string emailTo, string token)
        {
            string subject = "EZNOTES: Password reset";
            string emailContent = System.IO.File.ReadAllText(@"./template/passwordreset.txt");
            emailContent = emailContent.Replace("{{EMAIL}}", emailTo).Replace("{{CODE}}", token);

            SendGridClient client = new SendGridClient(this.sendGridSecret);
            EmailAddress from = new EmailAddress("lsk18mto@gmail.com");
            EmailAddress to = new EmailAddress(emailTo);
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, "", emailContent);

            await client.SendEmailAsync(msg);
        }
    }
}
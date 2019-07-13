using liivlabs_shared.Interfaces.SMTP;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

/// <summary>
/// Email Sender External Service
/// </summary>
namespace liivlabs_infrastructure.SMTP
{
    public class EmailSender: IEmailSender
    {
        /// <summary>
        /// Mail gun api key
        /// </summary>
        private string MAILGUN_API_KEY;

        /// <summary>
        /// Email From
        /// </summary>
        private string from;

        /// <summary>
        /// Inject Services
        /// </summary>
        public EmailSender(IConfiguration configuration)
        {
            this.MAILGUN_API_KEY = configuration["MAINGUN_APIKEY"];
            this.from = configuration["FROM"];
        }

        /// <summary>
        /// Send Verification Email to given User
        /// </summary>
        /// <param name="emailTo"></param>
        /// <returns></returns>
        public async Task<IRestResponse> SendEmailForVerification(string emailTo, string token)
        {
            //Get email template and insert email and token
            string emailContent = System.IO.File.ReadAllText(@"./template/emailverify.txt");
            emailContent = emailContent.Replace("{{EMAIL}}", emailTo).Replace("{{CODE}}",token);

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api", this.MAILGUN_API_KEY);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "mg.liivlabs.com", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", this.from);
            request.AddParameter("to", emailTo);
            request.AddParameter("subject", "Email Verification");
            request.AddParameter("html", emailContent);
            request.Method = Method.POST;
            return await client.ExecuteTaskAsync(request);

        }


        /// <summary>
        /// Send password reset link to given user
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IRestResponse>  SendPasswordResetEmail(string emailTo, string token)
        {
            string emailContent = System.IO.File.ReadAllText(@"./template/passwordreset.txt");
            emailContent = emailContent.Replace("{{EMAIL}}", emailTo).Replace("{{CODE}}", token);

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api", this.MAILGUN_API_KEY);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "mg.liivlabs.com", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", this.from);
            request.AddParameter("to", emailTo);
            request.AddParameter("subject", "Password reset");
            request.AddParameter("html", emailContent);
            request.Method = Method.POST;
            return await client.ExecuteTaskAsync(request);
        }
    }
}

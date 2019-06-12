using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Inteface to implement
/// Email Sender Service
/// </summary>
namespace liivlabs_shared.Interfaces.SMTP
{
    public interface IEmailSender
    {
        /// <summary>
        /// Send Email to given user for verification
        /// </summary>
        /// <param name="emailTo">Send email to given address</param>
        /// <param name="token">tokent to add</param>"
        /// <returns></returns>
        Task SendEmailForVerification(string emailTo, string token);
    }
}

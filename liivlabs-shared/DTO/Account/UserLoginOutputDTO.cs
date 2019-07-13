using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// User login response
/// </summary>
namespace liivlabs_shared.DTO.Account
{
    public class UserLoginOutputDTO
    {
        /// <summary>
        /// First Name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email Address of the user
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Email is verified or not
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// Token Issued to user
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// Token expiry time
        /// </summary>
        public DateTime Expire { get; set; }

        /// <summary>
        /// Role of the user
        /// </summary>
        public string Role { get; set; }
    }
}

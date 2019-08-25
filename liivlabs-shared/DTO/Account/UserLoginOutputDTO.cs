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
        /// User Id
        /// </summary>
        public int userId { get; set; }

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

        /// <summary>
        /// Password changed by user or not
        /// </summary>
        public bool PasswordChanged { get; set; }

        /// <summary>
        /// Max Users allowed for Business Users by Admin
        /// </summary>
        public int MaxUsers { get; set; }

        /// <summary>
        /// Added Users
        /// </summary>
        public int AddedUsers { get; set; }
    }
}

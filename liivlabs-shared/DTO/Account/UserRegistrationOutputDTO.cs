using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// User Registration Output DTO
/// </summary>
namespace liivlabs_shared.DTO.Account
{
    public class UserRegistrationOutputDTO
    {
        /// <summary>
        /// User Id assigned by database
        /// </summary>
        public string UserId { get; set; }

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
        /// Email verified or not
        /// </summary>
        public bool EmailVerified { get; set; }
    }
}

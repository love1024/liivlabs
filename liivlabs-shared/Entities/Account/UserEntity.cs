using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

/// <summary>
/// User Information Database
/// </summary>
namespace liivlabs_shared.Entities.Account
{
    public class UserEntity
    {   
        /// <summary>
        /// User Id Primary key
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// First Name of the User
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Salt to user for password
        /// </summary>
        public byte[] passwordSalt { get; set; }

        /// <summary>
        /// Hashing Generated using password and salt
        /// </summary>
        public byte[] passwordHash { get; set; }

        /// <summary>
        /// Email Address of the User
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Email is verified or not
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// Whether Use has reset the password or not
        /// </summary>
        public bool PasswordReset { get; set; }

        /// <summary>
        /// Is this User admin
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Parent User for Current user
        /// </summary>
        public int ParentUserId { get; set; }
    }
}

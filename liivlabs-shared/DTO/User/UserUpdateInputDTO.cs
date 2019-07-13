﻿using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Update User Input DTO
/// </summary>
namespace liivlabs_shared
{
    public class UserUpdateInputDTO
    {
        /// <summary>
        /// User Id assigned by database
        /// </summary>
        public int UserId { get; set; }

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

        /// <summary>
        /// Is User Admin
        /// </summary>
        public string Role { get; set; }

    }
}

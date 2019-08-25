using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_shared.DTO
{
    public class UserAddInputDTO
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
        /// Email verified or not
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// Is User Admin
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Max Users allowed for Business Users by Admin
        /// </summary>
        public int MaxUsers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

/// <summary>
/// DTO to handle user Input
/// </summary>
namespace liivlabs_shared.DTO.Account
{
    public class UserRegistrationInputDTO
    {
        /// <summary>
        /// First Name of the user
        /// </summary>
        [Required(ErrorMessage ="First Name is required")]
        [StringLength(20, ErrorMessage = "First Name should be less than 20 characters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of the user
        /// </summary>
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(20, ErrorMessage = "Last Name should be less than 20 characters")]
        public string LastName { get; set; }

        /// <summary>
        /// Password of the User
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Please enter valid Email")]
        public string EmailAddress { get; set; }
    }
}

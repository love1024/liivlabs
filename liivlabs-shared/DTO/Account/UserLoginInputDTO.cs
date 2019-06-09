using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

/// <summary>
/// User Login Input response
/// </summary>
namespace liivlabs_shared.DTO.Account
{
    public class UserLoginInputDTO
    {
        /// <summary>
        /// User Email Address
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}

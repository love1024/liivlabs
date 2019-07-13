using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_shared.DTO.Account
{
    public class UserResetPasswordInputDTO
    {
        /// <summary>
        /// Email address of the user
        /// </summary>
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// New password of the user 
        /// </summary>
        public string NewPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_shared.DTO.User
{
    public class UserPasswordChangeInputDTO
    {
        /// <summary>
        /// User id to delete
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// New password
        /// </summary>
        public string Password { get; set; }
    }
}

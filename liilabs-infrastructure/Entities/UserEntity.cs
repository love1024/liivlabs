using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace liivlabs_infrastructure.Entities
{
    public class UserEntity
    {   
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string HashingSalt { get; set; }

        public string EmailAddress { get; set; }
    }
}

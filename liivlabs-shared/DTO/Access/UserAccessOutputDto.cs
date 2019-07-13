using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_shared.DTO
{
    public class UserAccessOutputDto
    {
        /// <summary>
        /// Service name
        /// </summary>
        public string name { get; set; } = "Users";

        /// <summary>
        /// User Id Primary key
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Create access
        /// </summary>
        public bool Create { get; set; }

        /// <summary>
        /// Delete access
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// Edit access
        /// </summary>
        public bool Edit { get; set; }

        /// <summary>
        /// View Access
        /// </summary>
        public bool View { get; set; }
    }
}

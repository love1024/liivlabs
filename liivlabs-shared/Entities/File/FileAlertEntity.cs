using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace liivlabs_shared.Entities.File
{
    public class FileAlertEntity
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public bool AnyNew { get; set; }
    }
}

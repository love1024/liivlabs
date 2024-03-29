﻿using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_shared.DTO
{
    public class FileOutputDTO
    {
        public int FileId { get; set; }

        public string UserEmail { get; set; }

        public string OriginalName { get; set; }

        public decimal OriginalSize { get; set; }

        public string VideoFileName { get; set; }

        public string AudioFileName { get; set; }

        public string Text { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime editedAt { get; set; }

        public bool isNew { get; set; }
    }
}

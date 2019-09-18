using liivlabs_shared.Entities;
using liivlabs_shared.Entities.Account;
using liivlabs_shared.Entities.File;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_infrastructure.EntityFramework
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }


        public DbSet<UserEntity> User { get; set; }

        public DbSet<UserAccessEntity> UserAccess { get; set; }

        public DbSet<FileEntity> Files { get; set; }

        public DbSet<FileAlertEntity> FileAlert { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_infrastructure.Entities
{
    public class AccountContext: DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        { }


        public DbSet<UserEntity> UserEntities { get; set; }
    }
}

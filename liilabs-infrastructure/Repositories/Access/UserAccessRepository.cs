using liivlabs_infrastructure.EntityFramework;
using liivlabs_shared.Entities;
using liivlabs_shared.Interfaces.Repository.Access;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace liivlabs_infrastructure.Repositories.Access
{
    public class UserAccessRepository : IUserAccessRepository
    {
        private DatabaseContext context;

        public UserAccessRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<UserAccessEntity> GetUserAccess(int userId)
        {
           return await this.context.UserAccess.SingleOrDefaultAsync((access) => access.UserId == userId);
        }

        public async Task UpdateUserAccess(UserAccessEntity userAccessEntity)
        {
            UserAccessEntity user =  this.context.UserAccess.SingleOrDefault((access) => access.UserId == userAccessEntity.UserId);
            if(user == null)
            {
                this.context.UserAccess.Add(userAccessEntity);
            }
            else
            {
                user.Create = userAccessEntity.Create;
                user.Edit = userAccessEntity.Edit;
                user.View = userAccessEntity.View;
                user.Delete = userAccessEntity.Delete;
                this.context.UserAccess.Update(user);
            }
            await this.context.SaveChangesAsync();
        }
    }
}

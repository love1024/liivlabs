using liivlabs_infrastructure.EntityFramework;
using liivlabs_shared.Entities.Account;
using liivlabs_shared.Interfaces.Repository.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// User Repository
/// </summary>
namespace liivlabs_infrastructure.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Database context
        /// </summary>
        private DatabaseContext context;

        /// <summary>
        /// Get DB Context
        /// </summary>
        public UserRepository(DatabaseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public async Task<UserEntity> AddUser(UserEntity userEntity)
        {
            this.context.Add(userEntity);
            await this.context.SaveChangesAsync();

            return userEntity;
        }

        /// <summary>
        /// Delete given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeleteUser(int userId)
        {
            UserEntity userEntity = new UserEntity() { UserId = userId };
            this.context.User.Attach(userEntity);
            this.context.User.Remove(userEntity);
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Get List of All users
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UserEntity>> GetAllUsers()
        {
            return await this.context.User.ToListAsync();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async  Task<UserEntity> GetUser(int userId)
        {
            return await this.context.User.FirstOrDefaultAsync((user) => user.UserId == userId);
        }

        /// <summary>
        /// Update given user
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public async Task UpdateUser(UserEntity userEntity)
        {
            this.context.User.Update(userEntity);
            await this.context.SaveChangesAsync();
        }
    }
}

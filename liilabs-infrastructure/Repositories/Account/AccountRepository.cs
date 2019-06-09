using liivlabs_infrastructure.EntityFramework;
using liivlabs_shared.Entities.Account;
using liivlabs_shared.Interfaces.Repository.Account;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Account repository to handle 
/// user database
/// </summary>
namespace liivlabs_infrastructure.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Database context
        /// </summary>
        private DatabaseContext context;

        public AccountRepository(DatabaseContext accountContext)
        {
            this.context = accountContext;
        }

        /// <summary>
        /// Add new user to database
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>Task of User entity</returns>
        public async Task<UserEntity> AddUser(UserEntity user)
        {
            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Find user by given email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<UserEntity> FindUserByEmail(string emailAddress)
        {
            return await this.context.User.SingleOrDefaultAsync((user) => user.EmailAddress == emailAddress);
        }

        /// <summary>
        /// Check whether user exist or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> UserExist(string email)
        {
            return await this.context.User.AnyAsync((user) => user.EmailAddress == email);
        }
    }
}

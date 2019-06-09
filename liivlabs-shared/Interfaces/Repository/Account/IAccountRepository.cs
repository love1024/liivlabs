using liivlabs_shared.DTO.Account;
using liivlabs_shared.Entities.Account;
using System.Threading.Tasks;

/// <summary>
/// Interface to User Account
/// Repository for database
/// </summary>
namespace liivlabs_shared.Interfaces.Repository.Account
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Add New User to database
        /// </summary>
        /// <param name="user">User Input data</param>
        /// <returns>Newly Added User</returns>
        Task<UserEntity> AddUser(UserEntity user);

        /// <summary>
        /// Check if user exist in database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> UserExist(string email);

        /// <summary>
        /// Find User by email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<UserEntity> FindUserByEmail(string emailAddress);

    }
}

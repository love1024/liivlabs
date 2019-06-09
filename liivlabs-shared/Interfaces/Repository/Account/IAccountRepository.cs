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

        Task<bool> UserExist(string email);

    }
}

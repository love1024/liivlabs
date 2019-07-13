using liivlabs_shared.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// user Repository
/// </summary>
namespace liivlabs_shared.Interfaces.Repository.User
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get All User entities
        /// </summary>
        /// <returns></returns>
        Task<IList<UserEntity>> GetAllUsers();

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        Task<UserEntity> AddUser(UserEntity userEntity);

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task UpdateUser(UserEntity userEntity);

        /// <summary>
        /// Delete given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteUser(int userId);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserEntity> GetUser(int userId);
    }
}

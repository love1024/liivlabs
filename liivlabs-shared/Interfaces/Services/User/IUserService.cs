using liivlabs_shared.DTO;
using liivlabs_shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// User Service
/// </summary>
namespace liivlabs_shared.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Get a list of all users
        /// </summary>
        /// <returns></returns>
        Task<IList<UserOutoutDTO>> GetAllUsers(int userId);

        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="userAddInputDTO"></param>
        /// <returns></returns>
        Task<UserOutoutDTO> AddNewUser(UserAddInputDTO userAddInputDTO,int userId);

        /// <summary>
        /// Update Give User
        /// </summary>
        /// <param name="userUpdateInputDTO"></param>
        /// <returns></returns>
        Task UpdateUser(UserUpdateInputDTO userUpdateInputDTO,int userId);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userDeleteInputDTO"></param>
        /// <returns></returns>
        Task DeleteUser(UserDeleteInputDTO userDeleteInputDTO, int userId);
    }
}

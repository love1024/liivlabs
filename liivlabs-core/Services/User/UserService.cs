using AutoMapper;
using liivlabs_shared;
using liivlabs_shared.DTO;
using liivlabs_shared.DTO.User;
using liivlabs_shared.Entities.Account;
using liivlabs_shared.Interfaces.Repository.User;
using liivlabs_shared.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// User Service
/// </summary>
namespace liivlabs_core.Services.User
{
    public class UserService : IUserService
    {
        /// <summary>
        /// User Repository
        /// </summary>
        private IUserRepository userRepository;

        /// <summary>
        /// Auto mapper;
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// Get User Repository
        /// </summary>
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="userAddInputDTO"></param>
        /// <returns></returns>
        public async Task<UserOutoutDTO> AddNewUser(UserAddInputDTO userAddInputDTO)
        {
            UserEntity newUser = this.mapper.Map<UserEntity>(userAddInputDTO);
            UserEntity updatedUser = await this.userRepository.AddUser(newUser);

            return this.mapper.Map<UserOutoutDTO>(updatedUser);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userDeleteInputDTO"></param>
        /// <returns></returns>

        public async Task DeleteUser(UserDeleteInputDTO userDeleteInputDTO)
        {
            await this.userRepository.DeleteUser(userDeleteInputDTO.UserId);
        }

        /// <summary>
        /// Get List of all users
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UserOutoutDTO>> GetAllUsers(int userId)
        {
            IList<UserEntity> users = await this.userRepository.GetAllUsers();

            //Map to user output dto
            return this.mapper.Map<IList<UserOutoutDTO>>(users);
        }

        /// <summary>
        /// Update Given User
        /// </summary>
        /// <param name="userUpdateInputDTO"></param>
        /// <returns></returns>
        public async Task UpdateUser(UserUpdateInputDTO userUpdateInputDTO)
        {
            UserEntity foundUser = await this.userRepository.GetUser(userUpdateInputDTO.UserId);

            if(foundUser == null)
            {
                throw new ApplicationException("User not found");
            }

            foundUser.FirstName = userUpdateInputDTO.FirstName;
            foundUser.LastName = userUpdateInputDTO.LastName;
            foundUser.Role = userUpdateInputDTO.Role;
            foundUser.EmailAddress = userUpdateInputDTO.EmailAddress;
            foundUser.EmailVerified = userUpdateInputDTO.EmailVerified;

            await this.userRepository.UpdateUser(foundUser);
        }

    }
}

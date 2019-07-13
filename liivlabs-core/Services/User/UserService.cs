using AutoMapper;
using liivlabs_core.Helper;
using liivlabs_shared;
using liivlabs_shared.DTO;
using liivlabs_shared.DTO.User;
using liivlabs_shared.Entities;
using liivlabs_shared.Entities.Account;
using liivlabs_shared.Interfaces.Repository.Access;
using liivlabs_shared.Interfaces.Repository.Auth;
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
        /// User Access Repository
        /// </summary>
        private IUserAccessRepository userAccessRepository;

        /// <summary>
        /// Auto mapper;
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// Auth Service for JWT
        /// </summary>
        private IAuthService authService;

        /// <summary>
        /// Get User Repository
        /// </summary>
        public UserService(IUserRepository userRepository, IUserAccessRepository userAccessRepository, IMapper mapper, IAuthService authService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userAccessRepository = userAccessRepository;
            this.authService = authService;
        }

        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="userAddInputDTO"></param>
        /// <returns></returns>
        public async Task<UserOutoutDTO> AddNewUser(UserAddInputDTO userAddInputDTO, int userId)
        {
            UserEntity newUser = this.mapper.Map<UserEntity>(userAddInputDTO);
            bool hasAccessToCreate = true;
            UserEntity loggedInUser = await this.userRepository.GetUser(userId);

            byte[] passwordHash, passwordSalt;
            this.authService.CreatePasswordHash(newUser.EmailAddress, out passwordHash, out passwordSalt);
            newUser.passwordSalt = passwordSalt;
            newUser.passwordHash = passwordHash;

            if (loggedInUser.Role == Roles.BusinessUser)
            {
                UserAccessEntity userAccess = await this.userAccessRepository.GetUserAccess(userId);
                hasAccessToCreate = userAccess != null ? userAccess.Delete : false;
                newUser.ParentUserId = loggedInUser.ParentUserId;
            }

            if(loggedInUser.Role == Roles.Business)
            {
                newUser.ParentUserId = loggedInUser.UserId;
            }

            if (hasAccessToCreate)
            {
                UserEntity updatedUser = await this.userRepository.AddUser(newUser);
                return this.mapper.Map<UserOutoutDTO>(updatedUser);
            }
            else
            {
                throw new UnauthorizedAccessException("User don't have access to perform the add user operation");
            }
            
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userDeleteInputDTO"></param>
        /// <returns></returns>

        public async Task DeleteUser(UserDeleteInputDTO userDeleteInputDTO, int userId)
        {
            bool hasAccessToDelete = true;

            //Get the user that sent the delete request
            UserEntity loggedInUser = await this.userRepository.GetUser(userId);
            if (loggedInUser.Role == Roles.BusinessUser)
            {
                UserAccessEntity userAccess = await this.userAccessRepository.GetUserAccess(userId);
                hasAccessToDelete = userAccess != null ? userAccess.Delete : false;

            }
            if (hasAccessToDelete)
            {
                await this.userRepository.DeleteUser(userDeleteInputDTO.UserId);
            }
            else
            {
                throw new UnauthorizedAccessException("User don't have access to perform delete operation");
            }
        }

        /// <summary>
        /// Get List of all users
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UserOutoutDTO>> GetAllUsers(int userId)
        {
            List<UserEntity> allUsers = null;

            //Get the user that sent the delete request
            UserEntity loggedInUser = await this.userRepository.GetUser(userId);
            allUsers = (List<UserEntity>)await this.userRepository.GetAllUsers();

            if (loggedInUser.Role == Roles.Admin)
            {
                allUsers = allUsers.FindAll(user => user.ParentUserId == loggedInUser.UserId);

            } else if(loggedInUser.Role == Roles.BusinessUser)
            {
                UserAccessEntity userAccess = await this.userAccessRepository.GetUserAccess(loggedInUser.UserId);
                if (userAccess.View)
                {
                    allUsers = allUsers.FindAll(user => user.ParentUserId == loggedInUser.ParentUserId);
                }
                else
                {
                    throw new UnauthorizedAccessException("User Don't Have access for this action");
                }

            } else if(loggedInUser.Role == Roles.User)
            {
                throw new UnauthorizedAccessException("User Don't Have access for this action");
            }

            //Map to user output dto
            return this.mapper.Map<IList<UserOutoutDTO>>(allUsers);
        }


        /// <summary>
        /// Update Given User
        /// </summary>
        /// <param name="userUpdateInputDTO"></param>
        /// <returns></returns>
        public async Task UpdateUser(UserUpdateInputDTO userUpdateInputDTO,int userId)
        {
            UserEntity foundUser = await this.userRepository.GetUser(userUpdateInputDTO.UserId);
            bool hasAccessToUpdate = true;

            if (foundUser == null)
            {
                throw new ApplicationException("User not found");
            }

            //Get the user that sent the delete request
            UserEntity loggedInUser = await this.userRepository.GetUser(userId);
            if (loggedInUser.Role == "BusinessUser")
            {
                UserAccessEntity userAccess = await this.userAccessRepository.GetUserAccess(userId);
                hasAccessToUpdate = userAccess != null ? userAccess.Edit : false;

            }

            if (hasAccessToUpdate)
            {
                foundUser.FirstName = userUpdateInputDTO.FirstName;
                foundUser.LastName = userUpdateInputDTO.LastName;
                foundUser.Role = userUpdateInputDTO.Role;
                foundUser.EmailAddress = userUpdateInputDTO.EmailAddress;
                foundUser.EmailVerified = userUpdateInputDTO.EmailVerified;

                await this.userRepository.UpdateUser(foundUser);
            }
            else
            {
                throw new UnauthorizedAccessException("User don't have access to perform Edit operation");
            }
        }

    }
}

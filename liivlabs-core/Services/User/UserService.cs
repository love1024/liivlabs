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
using liivlabs_shared.Interfaces.Services.Access;
using liivlabs_shared.Interfaces.Services.Account;
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
        /// Access service
        /// </summary>
        private IAccountService accountService;

        /// <summary>
        /// Get User Repository
        /// </summary>
        public UserService(
            IUserRepository userRepository,
            IUserAccessRepository userAccessRepository,
            IMapper mapper,
            IAuthService authService,
            IAccountService accountService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userAccessRepository = userAccessRepository;
            this.authService = authService;
            this.accountService = accountService;
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
                UserEntity parent = await this.userRepository.GetUser(loggedInUser.ParentUserId);
                if (parent.AddedUsers + 1 > parent.MaxUsers)
                {
                    throw new ApplicationException("Business cannot add more users");
                }
                // Update user added users
                parent.AddedUsers = parent.AddedUsers + 1;
                await this.userRepository.UpdateUser(parent);

                UserAccessEntity userAccess = await this.userAccessRepository.GetUserAccess(userId);
                hasAccessToCreate = userAccess != null ? userAccess.Delete : false;
                newUser.ParentUserId = loggedInUser.ParentUserId;
            }

            if(loggedInUser.Role == Roles.Business)
            {
                newUser.ParentUserId = loggedInUser.UserId;
                if (loggedInUser.AddedUsers + 1 > loggedInUser.MaxUsers)
                {
                    throw new ApplicationException("Business cannot add more users");
                }
            }

            if (hasAccessToCreate)
            {
                UserEntity updatedUser = await this.userRepository.AddUser(newUser);
                if(!updatedUser.EmailVerified)
                {
                    await this.accountService.SendVerificationEmail(updatedUser.EmailAddress);
                }

                if (loggedInUser.Role == Roles.Business)
                {
                    // Update user added users
                    loggedInUser.AddedUsers = loggedInUser.AddedUsers + 1;
                    await this.userRepository.UpdateUser(loggedInUser);
                }

                return this.mapper.Map<UserOutoutDTO>(updatedUser);
            }
            else
            {
                throw new UnauthorizedAccessException("User don't have access to perform the add user operation");
            }
            
        }

        /// <summary>
        /// Change User password
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task ChangePassword(UserPasswordChangeInputDTO inputDto)
        {
            UserEntity foundUser = await this.userRepository.GetUser(inputDto.UserId);

            byte[] passwordHash, passwordSalt;
            this.authService.CreatePasswordHash(inputDto.Password, out passwordHash, out passwordSalt);
            foundUser.passwordSalt = passwordSalt;
            foundUser.passwordHash = passwordHash;
            foundUser.PasswordChanged = true;

            await this.userRepository.UpdateUser(foundUser);
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

            if (loggedInUser.Role == Roles.Business)
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
        /// Get User info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserOutoutDTO> GetUserInfo(int userId)
        {
            UserEntity user =  await this.userRepository.GetUser(userId);
            return this.mapper.Map<UserOutoutDTO>(user);
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
                foundUser.MaxUsers = userUpdateInputDTO.MaxUsers;

                await this.userRepository.UpdateUser(foundUser);
            }
            else
            {
                throw new UnauthorizedAccessException("User don't have access to perform Edit operation");
            }
        }

    }
}

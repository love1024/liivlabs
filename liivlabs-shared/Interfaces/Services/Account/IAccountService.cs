using liivlabs_shared.DTO.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// User Account Service Interface
/// </summary>
namespace liivlabs_shared.Interfaces.Services.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// Handle Add User Request
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<UserRegistrationOutputDTO> AddUser(UserRegistrationInputDTO userRegistrationInput);

        /// <summary>
        /// Handle User Authentication
        /// </summary>
        /// <param name="userLoginInput"></param>
        /// <returns></returns>
        Task<UserLoginOutputDTO> Authenticate(UserLoginInputDTO userLoginInput);

        /// <summary>
        /// Send Email for verification to given email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task SendVerificationEmail(string email);
    }
}

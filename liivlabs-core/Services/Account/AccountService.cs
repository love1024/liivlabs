using AutoMapper;
using liivlabs_shared.DTO.Account;
using liivlabs_shared.Entities.Account;
using liivlabs_shared.Interfaces.Repository.Account;
using liivlabs_shared.Interfaces.Repository.Auth;
using liivlabs_shared.Interfaces.Services.Account;
using liivlabs_shared.Interfaces.SMTP;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Account Service to handle 
/// User Account Authentication
/// </summary>
namespace liivlabs_core.Services.Account
{
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Account Repository
        /// </summary>
        private IAccountRepository accountRepository;

        /// <summary>
        /// Mapper
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// Configuration 
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// Client for sending Emails
        /// </summary>
        private IEmailSender emailSender;

        /// <summary>
        /// Auth Service for JWT
        /// </summary>
        private IAuthService authService;

        /// <summary>
        /// Inject services
        /// </summary>
        public AccountService(
            IAccountRepository accountRepository,
            IMapper mapper,
            IConfiguration configuration,
            IAuthService authService,
            IEmailSender emailSender)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
            this.configuration = configuration;
            this.emailSender = emailSender;
            this.authService = authService;
        }
        /// <summary>
        /// Handle Add New User request
        /// </summary>
        /// <param name="userRegistrationInput"></param>
        /// <returns></returns>
        public async Task<UserRegistrationOutputDTO> AddUser(UserRegistrationInputDTO userRegistrationInput)
        {
            string password = userRegistrationInput.Password;
                
            //Check if user already exist
            bool isAlreadyExist = await this.accountRepository.UserExist(userRegistrationInput.EmailAddress);
            if(isAlreadyExist) {
                throw new ApplicationException("Email " + userRegistrationInput.EmailAddress + " already exist");
            }

            byte[] passwordHash, passwordSalt;
            this.authService.CreatePasswordHash(password,out passwordHash,out passwordSalt);

            UserEntity user = this.mapper.Map<UserEntity>(userRegistrationInput);
            user.passwordSalt = passwordSalt;
            user.passwordHash = passwordHash;
            
            //Saved User with an ID
            UserEntity savedUser = await this.accountRepository.AddUser(user);
            return this.mapper.Map<UserRegistrationOutputDTO>(savedUser);
        }

        /// <summary>
        /// Handle User Authentication
        /// </summary>
        /// <param name="userLoginInput"></param>
        /// <returns></returns>
        public async Task<UserLoginOutputDTO> Authenticate(UserLoginInputDTO userLoginInput)
        {
            if(string.IsNullOrWhiteSpace(userLoginInput.EmailAddress) || string.IsNullOrWhiteSpace(userLoginInput.Password))
            {
                return null;
            }

            //Get user by given email id
            UserEntity foundUser = await this.accountRepository.FindUserByEmail(userLoginInput.EmailAddress);
            if (foundUser == null) {
                return null;
            }

            bool isValid = this.authService.VerifyPasswordHash(userLoginInput.Password, foundUser.passwordHash, foundUser.passwordSalt);

            if(isValid == false)
            {
                return null;
            }

            //Generate new token
            string token = this.authService.IssueNewToken();
            return new UserLoginOutputDTO()
            {
                EmailAddress = foundUser.EmailAddress,
                FirstName = foundUser.FirstName,
                LastName = foundUser.LastName,
                token = token
            };
        }

        /// <summary>
        /// Send Verification email to given user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task SendVerificationEmail(string email)
        {
            string token = this.authService.IssueNewToken();

            await this.emailSender.SendEmailForVerification(email, token);
        }

    }
}

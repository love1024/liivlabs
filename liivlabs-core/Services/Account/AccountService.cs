using AutoMapper;
using liivlabs_shared.DTO.Account;
using liivlabs_shared.Entities.Account;
using liivlabs_shared.Interfaces.Repository.Account;
using liivlabs_shared.Interfaces.Services.Account;
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
        /// Inject services
        /// </summary>
        public AccountService(IAccountRepository accountRepository, IMapper mapper, IConfiguration configuration)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        /// <summary>
        /// Handle Add New User request
        /// </summary>
        /// <param name="userRegistrationInput"></param>
        /// <returns></returns>
        public async Task<UserRegistrationOutputDTO> AddUser(UserRegistrationInputDTO userRegistrationInput)
        {
            //TODO Password Hashing and Salt
            string password = userRegistrationInput.Password;
            
            //Check if user already exist
            bool isAlreadyExist = await this.accountRepository.UserExist(userRegistrationInput.EmailAddress);
            if(isAlreadyExist) {
                throw new ApplicationException("Email " + userRegistrationInput.EmailAddress + " already exist");
            }

            UserEntity user = this.mapper.Map<UserEntity>(userRegistrationInput);
            
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

            //TODO Verify User password hash

            return this.IssueNewToken(foundUser);
        }

        /// <summary>
        /// Issue a new token to user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private UserLoginOutputDTO IssueNewToken(UserEntity user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(this.configuration["Secret"]);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return new UserLoginOutputDTO()
            {
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                token = tokenString
            };
        }
    }
}

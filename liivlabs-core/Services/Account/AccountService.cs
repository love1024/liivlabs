using AutoMapper;
using liivlabs_shared.DTO.Account;
using liivlabs_shared.Entities.Account;
using liivlabs_shared.Interfaces.Repository.Account;
using liivlabs_shared.Interfaces.Services.Account;
using System;
using System.Collections.Generic;
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
        /// Inject services
        /// </summary>
        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
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

            UserEntity user = this.mapper.Map<UserEntity>(userRegistrationInput);
            
            //Saved User with an ID
            UserEntity savedUser = await this.accountRepository.AddUser(user);
            return this.mapper.Map<UserRegistrationOutputDTO>(savedUser);
        }
    }
}

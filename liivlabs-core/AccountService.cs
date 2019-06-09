using liivlabs_core.DTO;
using liivlabs_core.Interfaces.Repository;
using liivlabs_core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_core
{
    public class AccountService : IAccountService
    {
        IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public string AddUser(UserInputDTO user)
        {
            string retVal = string.Empty;
            this.accountRepository.AddUser(user);

            return retVal;
        }
    }
}

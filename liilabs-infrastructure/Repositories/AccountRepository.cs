using liivlabs_core.DTO;
using liivlabs_core.Interfaces.Repository;
using liivlabs_infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private AccountContext accountContext;

        public AccountRepository(AccountContext accountContext)
        {
            this.accountContext = accountContext;
        }

        public bool AddUser(UserInputDTO user)
        {
            //EF code to add user
            //Map UserInputDTO to Entity 
            //AddUser
            UserEntity userEntity = new UserEntity();
            userEntity.EmailAddress = user.EmailAddress;
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Password = user.Password;

            this.accountContext.Add(userEntity);
           
            return true;
        }

    }
}

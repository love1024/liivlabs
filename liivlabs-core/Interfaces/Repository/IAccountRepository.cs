using liivlabs_core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_core.Interfaces.Repository
{
    public interface IAccountRepository
    {
        bool AddUser(UserInputDTO user);

    }
}

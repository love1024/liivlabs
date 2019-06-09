using liivlabs_core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_core.Interfaces.Services
{
    public interface IAccountService
    {
        string AddUser(UserInputDTO user);
    }
}

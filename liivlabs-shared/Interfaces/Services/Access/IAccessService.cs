using liivlabs_shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace liivlabs_shared.Interfaces.Services.Access
{
    public interface IAccessService
    {
        Task<AccessOutputDto> GetAllUserAccess(int userId);

        Task UpdateAllAccess(AccessOutputDto accessOutputDto);
    }
}

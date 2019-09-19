using liivlabs_shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace liivlabs_shared.Interfaces.Repository
{
    public interface IFileAlertRepository
    {
        Task<bool> CheckNewFile(string email);

        Task UpdateStatusForUser(string email,bool status);
    }
}

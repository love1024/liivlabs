using liivlabs_shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace liivlabs_shared.Interfaces.Repository.Access
{
    public interface IUserAccessRepository
    {
        /// <summary>
        /// Get User Access of given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserAccessEntity> GetUserAccess(int userId);

        /// <summary>
        /// User Access Update
        /// </summary>
        /// <param name="userAccessEntity"></param>
        /// <returns></returns>
        Task UpdateUserAccess(UserAccessEntity userAccessEntity);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Service to handle Auth
/// </summary>
namespace liivlabs_shared.Interfaces.Repository.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Issue new JWT token
        /// </summary>
        /// <returns></returns>
        string IssueNewToken();

        /// <summary>
        /// Create password Hash with generated
        /// salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        /// <summary>
        /// Verify given password using stored password hash
        /// and salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}

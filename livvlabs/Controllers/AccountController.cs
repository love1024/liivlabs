using System;
using System.Threading.Tasks;
using liivlabs_core.Helper;
using liivlabs_shared.DTO.Account;
using liivlabs_shared.Interfaces.Services.Account;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// User Account controller to handle 
/// user authentication
/// </summary>
namespace liivlabs.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Account Service
        /// </summary>
        IAccountService accountService;

        /// <summary>
        /// Constructor To Inject service
        /// </summary>
        /// <param name="accountService">Account Service</param>
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="user">Input user data</param>
        [HttpPost("register")]
        public async Task<ActionResult<UserRegistrationOutputDTO>> AddNewUser([FromBody] UserRegistrationInputDTO userRegistrationInput)
        {
            if (!ModelState.IsValid) {
                return StatusCode(400, Helper.FormatErrorResponse(ModelState));
            }
            try
            {
                return await accountService.AddUser(userRegistrationInput);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

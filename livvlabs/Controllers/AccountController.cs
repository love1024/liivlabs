using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using liivlabs_core.Helper;
using liivlabs_shared.DTO.Account;
using liivlabs_shared.Interfaces.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// User Account controller to handle 
/// user authentication
/// </summary>
namespace liivlabs.Controllers
{
    [Route("api/account")]
    [Authorize]
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
        [AllowAnonymous]
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
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }
        
        /// <summary>
        /// Authenticate User by email and password
        /// Issue a token if valid
        /// </summary>
        /// <param name="userLoginInput">User login data</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginOutputDTO>> Authenticate([FromBody] UserLoginInputDTO userLoginInput)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, Helper.FormatErrorResponse(ModelState));
            }
            try
            {
                UserLoginOutputDTO user  = await this.accountService.Authenticate(userLoginInput);
                if(user == null)
                {
                    return BadRequest(new { message = new List<string> { "Username or Password is incorrect" } });
                }
                return user;
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }

        /// <summary>
        /// Send Email for verification
        /// </summary>
        /// <returns></returns>
        [HttpPost("sendverification")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSendEmailVerificationOutputDTO>> SendEmailVerification([FromBody] UserSendEmailVerificationInputDTO userSendVerfiyEmailInput)
        {
            try
            {
                await this.accountService.SendVerificationEmail(userSendVerfiyEmailInput.EmailTo);
                return new UserSendEmailVerificationOutputDTO() { success = true};
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }

        /// <summary>
        /// Verify email verification
        /// </summary>
        /// <param name="userVerifyEmailInput"></param>
        /// <returns></returns>
        [HttpPost("verifyemail")]
        public async Task<ActionResult<UserVerifyEmailOutputDTO>> VerifyEmail([FromBody] UserVerifyEmailInputDTO userVerifyEmailInput)
        {
            try
            {
                UserVerifyEmailOutputDTO result = await this.accountService.SetEmailVerified(userVerifyEmailInput);
                if(result == null)
                {
                    return BadRequest(new { message = new List<string> { "Email already Verified or user not found" } });
                }
                return result;
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liivlabs_core.Helper;
using liivlabs_shared;
using liivlabs_shared.DTO;
using liivlabs_shared.DTO.Success;
using liivlabs_shared.DTO.User;
using liivlabs_shared.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// User Controller
/// </summary>

namespace liivlabs.Controllers
{
    [Route("api/user")]
    [Authorize (Roles = Roles.Admin + "," + Roles.Business + "," + Roles.BusinessUser)]
    public class UserController : Controller
    {
        private IUserService userService;

        /// <summary>
        /// Get user service
        /// </summary>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get all list of all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
       
        public async Task<ActionResult<IList<UserOutoutDTO>>> GetAllUsers(int userId)
        {
            try
            {
                IList<UserOutoutDTO> users = await this.userService.GetAllUsers(userId);
                return users.ToList();
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]

        public async Task<ActionResult<UserOutoutDTO>> AddNewUser([FromBody] UserAddInputDTO userAddInputDTO, int userId)
        {
            try
            {
                return await this.userService.AddNewUser(userAddInputDTO,userId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }


        /// <summary>
        /// Update user
        /// </summary>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<ActionResult<CommonSuccessMessageOutputDTO>> UpdateUser([FromBody] UserUpdateInputDTO userUpdateInputDTO, int userId)
        {
            try
            {
                await this.userService.UpdateUser(userUpdateInputDTO, userId);
                return new CommonSuccessMessageOutputDTO() { Success = true };
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<CommonSuccessMessageOutputDTO>> DeleteUser([FromBody] UserDeleteInputDTO userDeleteInputDTO, int userId)
        {
            try
            {   
                await this.userService.DeleteUser(userDeleteInputDTO, userId);
                return new CommonSuccessMessageOutputDTO() { Success = true };
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }
    }
}

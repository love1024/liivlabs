using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liivlabs_shared.DTO;
using liivlabs_shared.DTO.Success;
using liivlabs_shared.Interfaces.Services.Access;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace liivlabs.Controllers
{
    [Route("api/access")]
    public class AccessController : Controller
    {
        private IAccessService accessService;

        public AccessController(IAccessService accessService)
        {
            this.accessService = accessService;
        }

        [HttpGet]
        public async Task<ActionResult<AccessOutputDto>> GetUserAllAccess(int userId)
        {
            try
            {
                return await this.accessService.GetAllUserAccess(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommonSuccessMessageOutputDTO>> UpdateUserAccess([FromBody] AccessOutputDto accessOutputDto)
        {
            try
            {
               await this.accessService.UpdateAllAccess(accessOutputDto);
               return new CommonSuccessMessageOutputDTO() { Success = true };
            }
            catch (Exception ex)
            {
               return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }
    }
}

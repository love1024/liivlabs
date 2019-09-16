using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using liivlabs_shared.Interfaces;
using liivlabs_shared.DTO;
using System.Collections.Generic;

namespace liivlabs.Controllers
{
    [Route("api/file")]
    public class FileController : Controller
    {
        /** File Service */
        private IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }
        
        [HttpPost("upload")]
        public async Task uploadFile([FromForm] IFormFile file, string email)
        {
            try
            {
                await this.fileService.SpeechToText(file, email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [HttpGet("files")]
        public async Task<ActionResult<List<FileOutputDTO>>> GetFilesOfUser(string email)
        {
            try
            {
                return await this.fileService.GetFilesOfUser(email);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }

        [HttpGet("fileurl")]
        public async Task<ActionResult<FileURLOutputDTO>> GetFileUrl(string filename)
        {
            try
            {
                return await this.fileService.GetFileUrl(filename);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }

    }
}

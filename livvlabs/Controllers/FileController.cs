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

        [HttpGet("file")]
        public async Task<ActionResult<FileOutputDTO>> GetFile(string filename)
        {
            try
            {
                return await this.fileService.GetFileAsync(filename);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = new List<string> { ex.Message } });
            }
        }

        [HttpGet("alert")]
        public async Task<bool> CheckNewFile(string email)
        {
            try
            {
                return await this.fileService.CheckNewFile(email);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost("alert")]
        public async Task<String> UpdateStatusForUser([FromBody] FileAlertDTO fileAlertDTO)
        {
            try
            {
                return await this.fileService.UpdateStatusForUser(fileAlertDTO.Email, fileAlertDTO.AnyNew);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpGet("delete")]
        public async Task DeleteFile(int id)
        {
            try
            {
                await this.fileService.DeleteFile(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [HttpPost("filename")]
        public async Task ChangeName([FromBody] FileTextDTO fileTextDTO, int fileId)
        {
            try
            {
                await this.fileService.ChangeName(fileId, fileTextDTO.Text);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [HttpPost("changeFileText")]
        public async Task<String> ChangeFileText([FromBody] FileTextDTO fileTextDTO, int fileId)
        {
            try
            {
                return await this.fileService.ChangeFileText(fileTextDTO.Text, fileId);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        [HttpPost("update")]
        public async Task UpdateFile([FromBody] FileOutputDTO file)
        {
            try
            {
                await this.fileService.UpdateFile(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

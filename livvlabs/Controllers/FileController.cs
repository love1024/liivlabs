using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using liivlabs_shared.Interfaces;

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
        public void uploadFile([FromForm] IFormFile file, string email)
        {
            try
            {
                this.fileService.SpeechToText(file, email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

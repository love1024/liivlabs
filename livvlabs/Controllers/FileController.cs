using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xabe.FFmpeg;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Google.Apis.Auth.OAuth2;
using liivlabs_shared.Interfaces.Services;

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
        public async Task uploadFile([FromForm] IFormFile file)
        {
            try
            {
                string fullPath = await this.fileService.SaveFile(file);
                string audioFile = await this.fileService.ConvertToAudioFile(fullPath);

                LongRunningRecognizeResponse respone =  await this.fileService.ConvertSpeechFileToText(audioFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

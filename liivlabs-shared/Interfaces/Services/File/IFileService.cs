using Google.Cloud.Speech.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/** 
 * File Service
 */
namespace liivlabs_shared.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file);

        Task<string> ConvertToAudioFile(string filePath);

        Task<LongRunningRecognizeResponse> ConvertSpeechFileToText(string filePath);
    }
}

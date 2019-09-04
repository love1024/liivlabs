using Google.Cloud.Speech.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/** 
 * File Service
 */
namespace liivlabs_shared.Interfaces
{
    public interface IFileService
    {
        Task SpeechToText(IFormFile file, string userEmail);

        Task<string> SaveFile(IFormFile file, string name);

        Task<string> ConvertToAudioFile(string filePath);

        Task<Google.Apis.Storage.v1.Data.Object> SaveFileToGoogleCloud(string filePath, string name);

        Task<string> ConvertSpeechFileToText(string filePath);
    }
}

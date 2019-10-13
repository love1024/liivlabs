using Google.Cloud.Speech.V1;
using liivlabs_shared.DTO;
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

        Task<List<FileOutputDTO>> GetFilesOfUser(string email);

        Task<FileURLOutputDTO> GetFileUrl(string filename);

        Task<FileOutputDTO> GetFileAsync(string filename);

        Task<bool> CheckNewFile(string email);

        Task<String> UpdateStatusForUser(string email, bool status);

        Task<String> ChangeFileText(string text, int id);

        Task DeleteFile(int id);

        Task ChangeName(int id, string name);

        Task UpdateFile(FileOutputDTO file);
    }
}

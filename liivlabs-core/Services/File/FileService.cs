using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Xabe.FFmpeg;
//using Google.Cloud.Speech.V1;
using Google.Cloud.Speech.V1P1Beta1;
using Grpc.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System;
using liivlabs_shared.Entities.File;
using liivlabs_shared.Interfaces;
using liivlabs_shared.Interfaces.Repository;
using liivlabs_shared.DTO;
using System.Collections.Generic;
using AutoMapper;
using System.Net.Http;

/**
 * File Service 
 */
namespace liivlabs_core.Services
{
    public class FileService : IFileService
    {
        private IFileRepository fileRepository;

        private string folderPath = "files";

       // private string ffmpegPath = "/usr/bin";

       // private string keypath = "./key/test.json";

        private string ffmpegPath = "D:/ffmpeg/ffmpeg/bin";

        private string keypath = "D:/test/test.json";

        private string audioFileExtension = ".raw";

        private IMapper mapper;

        string bucketName = "eznotes-user-files";

        public FileService(IFileRepository fileRepository, IMapper mapper)
        {
            this.fileRepository = fileRepository;
            this.mapper = mapper;
        }

        public async Task SpeechToText(IFormFile file, string userEmail)
        {
            string originalName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string extension = Path.GetExtension(originalName);
            string uniqueName = Guid.NewGuid().ToString();
            string videoFileName = uniqueName + '.' + extension;
            string audioFileName = uniqueName + ".raw";

            // Store file local first to convert to audio
            string videoFilefullPath = await this.SaveFile(file, videoFileName);

            // Convert local store file to audio file and store it local also
            string audioSavedFile = await this.ConvertToAudioFile(videoFilefullPath);

            // Save both original and audio file to google cloud
            await this.SaveFileToGoogleCloud(audioSavedFile, audioFileName);
            await this.SaveFileToGoogleCloud(videoFilefullPath, videoFileName);

            // Now use cloud uploaded audio file to convert it into text using google speech to text
            string response = await this.ConvertSpeechFileToText(audioFileName);

            // Delete local file as already uploaded to google cloud
            File.Delete(audioSavedFile);
            File.Delete(videoFilefullPath);

            // Store information about file in database
            FileEntity fileToSave = new FileEntity()
            {
                AudioFileName = audioFileName,
                OriginalName = originalName,
                Text = response,
                UserEmail = userEmail,
                VideoFileName = videoFileName,
                OriginalSize = file.Length,
                createdAt = DateTime.Now,
                editedAt = DateTime.Now,
                isNew = true
            };

            await this.fileRepository.SaveFile(fileToSave);
        }

        public async Task<string> SaveFile(IFormFile file, string name)
        {
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), this.folderPath);
            if (file.Length > 0)
            {
                string fullPath = Path.Combine(pathToSave, name);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                   await file.CopyToAsync(stream);
                }

                return fullPath;
            }

            return "";
        }

        public async Task<Google.Apis.Storage.v1.Data.Object> SaveFileToGoogleCloud(string filePath, string name)
        {
            var credential = GoogleCredential.FromFile(this.keypath);
            var storage = StorageClient.Create(credential);

            using (var stream = File.OpenRead(filePath))
            {
                var response =  await storage.UploadObjectAsync(this.bucketName, name, null, stream);
                return response;
            }
        }

        public async Task<string> ConvertToAudioFile(string filePath)
        {
            FFmpeg.ExecutablesPath = this.ffmpegPath;

            //Get latest version of FFmpeg. It's great idea if you don't know if you had installed FFmpeg.
            // await FFmpeg.GetLatestVersion();

            //Save file to the same location with changed extension
            string outputFileName = Path.ChangeExtension(filePath, this.audioFileExtension);

            //Extra parameter to convert into format required for google speech to text
            await Conversion.ExtractAudio(filePath, outputFileName)
                    .AddParameter("-f s16le")
                    .AddParameter("-acodec pcm_s16le")
                    .AddParameter("-vn")
                    .AddParameter("-ac 1")
                    .AddParameter("-ar 16k")
                    .AddParameter("-map_metadata -1")
                    .Start();

            return outputFileName;
        }

        public async Task<string> ConvertSpeechFileToText(string fileName)
        {
            string URI = "gs://eznotes-user-files/" + fileName;

            // Create credential from secret file
            var credential = GoogleCredential.FromFile(this.keypath)
                            .CreateScoped(SpeechClient.DefaultScopes);
            var channel = new Grpc.Core.Channel(SpeechClient.DefaultEndpoint.ToString(),
                            credential.ToChannelCredentials());

            var speech = SpeechClient.Create(channel);
            var longOperation = await speech.LongRunningRecognizeAsync(new RecognitionConfig()
                            {
                                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                                SampleRateHertz = 16000,
                                LanguageCode = "en-US",
                                EnableWordTimeOffsets = true,
                                Model = "video",
                                EnableSpeakerDiarization = true,
                                EnableAutomaticPunctuation = true
                            }, RecognitionAudio.FromStorageUri(URI));

            longOperation = await longOperation.PollUntilCompletedAsync();
            string response = JsonConvert.SerializeObject(longOperation.Result.Results);
            
            return response;
        }

        public async Task<List<FileOutputDTO>> GetFilesOfUser(string email)
        {
            var list = await this.fileRepository.GetFileOfUser(email);

            return this.mapper.Map<List<FileEntity>, List<FileOutputDTO>>(list);
        }

        public async Task<FileURLOutputDTO> GetFileUrl(string filename)
        {
            using (Stream stream = new FileStream(this.keypath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var credential = ServiceAccountCredential.FromServiceAccountData(stream);
                UrlSigner urlSigner = UrlSigner.FromServiceAccountCredential(credential);
                var url = await urlSigner.SignAsync(this.bucketName, filename, TimeSpan.FromHours(1), HttpMethod.Get);
                return new FileURLOutputDTO()
                {
                    Url = url
                };
            }
        }

        public async Task<FileOutputDTO> GetFileAsync(string filename)
        {
            var file = await this.fileRepository.GetFile(filename);

            return this.mapper.Map<FileOutputDTO>(file);
        }

        public async Task<bool> CheckNewFile(string email)
        {
            return await this.fileRepository.CheckNewFile(email);
        }

        public async Task<string> ChangeFileText(string text, int id)
        {
            await this.fileRepository.ChangeFileText(text, id);
            return "";
        }

        public async Task DeleteFile(int id)
        {
            await this.fileRepository.DeleteFile(id);
        }

        public async Task ChangeName(int id, string name)
        {
            await this.fileRepository.ChangeName(id, name);
        }

        public async Task UpdateFile(FileOutputDTO file)
        {
            FileEntity entity = this.mapper.Map<FileEntity>(file);
            entity.isNew = false;
            await this.fileRepository.UpdateFile(entity);
        }

    }

}


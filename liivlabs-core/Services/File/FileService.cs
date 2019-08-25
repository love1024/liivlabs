using liivlabs_shared.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Google.Apis.Auth.OAuth2;

/**
 * File Service 
 */
namespace liivlabs_core.Services
{
    public class FileService : IFileService
    {
        private string folderPath = "files";

        private string ffmpegPath = "D:/ffmpeg/ffmpeg/bin/";

        private string audioFileExtension = ".raw";

        public async Task<string> SaveFile(IFormFile file)
        {
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), this.folderPath);
            if (file.Length > 0)
            {
                string originalName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string extension = Path.GetExtension(originalName);
                string filename = Guid.NewGuid().ToString() + '.' + extension;
                string fullPath = Path.Combine(pathToSave, filename);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return fullPath;
            }

            return "";
        }

        public async Task<string> ConvertToAudioFile(string filePath)
        {
            FFmpeg.ExecutablesPath = this.ffmpegPath;

            //Get latest version of FFmpeg. It's great idea if you don't know if you had installed FFmpeg.
            await FFmpeg.GetLatestVersion();

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

        public async Task<LongRunningRecognizeResponse> ConvertSpeechFileToText(string filePath)
        {
            string keyPath = "D:/test/test.json";

            // Create credential from secret file
            var credential = GoogleCredential.FromFile(keyPath)
                            .CreateScoped(SpeechClient.DefaultScopes);
            var channel = new Grpc.Core.Channel(SpeechClient.DefaultEndpoint.ToString(),
                            credential.ToChannelCredentials());

            var speech = SpeechClient.Create(channel);
            var longOperation = await speech.LongRunningRecognizeAsync(new RecognitionConfig()
                            {
                                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                                SampleRateHertz = 16000,
                                LanguageCode = "en",
                            }, RecognitionAudio.FromFile(filePath));

            longOperation = await longOperation.PollUntilCompletedAsync();
            return longOperation.Result;

//            foreach (var result in response.Results)
 //           {
   //             foreach (var alternative in result.Alternatives)
       //         {
    //                Console.WriteLine(alternative.Transcript);
     //           }
       //     }
        }
    }
}

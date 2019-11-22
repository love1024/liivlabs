using liivlabs_shared.DTO;
using liivlabs_shared.Entities.File;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace liivlabs_shared.Interfaces.Repository
{
    public interface IFileRepository
    {
        Task SaveFile(FileEntity file);

        Task<List<FileEntity>> GetFileOfUser(string email);

        Task<FileEntity> GetFile(string filename);

        Task ChangeFileText(string text, int id);

        Task DeleteFile(int id);

        Task ChangeName(int id, string name);

        Task UpdateFile(FileEntity file);

        Task<bool> CheckNewFile(string email);
    }
}

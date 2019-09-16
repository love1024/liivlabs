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
    }
}

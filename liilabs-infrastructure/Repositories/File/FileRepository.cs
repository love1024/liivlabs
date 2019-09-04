using liivlabs_infrastructure.EntityFramework;
using liivlabs_shared.Entities.File;
using liivlabs_shared.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace liivlabs_infrastructure.Repositories.File
{
    public class FileRepository : IFileRepository
    {
        private DatabaseContext context;

        public FileRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task SaveFile(FileEntity file)
        {
            try
            {
                await this.context.Files.AddAsync(file);
                await this.context.SaveChangesAsync();
            }catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

using liivlabs_infrastructure.EntityFramework;
using liivlabs_shared.DTO;
using liivlabs_shared.Entities.File;
using liivlabs_shared.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace liivlabs_infrastructure.Repositories.FileAlert
{
    public class FileAlertRepository : IFileAlertRepository
    {
        private DatabaseContext context;

        public FileAlertRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<bool> CheckNewFile(string email)
        {
            var fileAlertEntity = await context.FileAlert.Where(fileAlert => fileAlert.Email == email).FirstOrDefaultAsync();
            return fileAlertEntity.AnyNew == true;
                
        }

        public async Task UpdateStatusForUser(string email, bool status)
        {
            try
            {
                var fileAlertEntity = await this.context.FileAlert.Where(f => f.Email == email).FirstOrDefaultAsync();
                if(fileAlertEntity!=null)
                {
                    fileAlertEntity.AnyNew = status;
                }
                else
                {
                    FileAlertEntity fa = new FileAlertEntity();
                    fa.AnyNew = status;
                    fa.Email = email;
                    await this.context.FileAlert.AddAsync(fa);
                }
                await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

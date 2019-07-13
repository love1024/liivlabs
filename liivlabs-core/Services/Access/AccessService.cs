using AutoMapper;
using liivlabs_shared.DTO;
using liivlabs_shared.Entities;
using liivlabs_shared.Interfaces.Repository.Access;
using liivlabs_shared.Interfaces.Services.Access;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace liivlabs_core.Services
{
    public class AccessService : IAccessService
    {
        private IUserAccessRepository userAccessRepository;

        private IMapper mapper;

        public AccessService(IUserAccessRepository userAccessRepository, IMapper mapper)
        {
            this.userAccessRepository = userAccessRepository;
            this.mapper = mapper;
        }

        public async Task<AccessOutputDto> GetAllUserAccess(int userId)
        {
            AccessOutputDto accessOutputDto = new AccessOutputDto();
            UserAccessEntity userAccessEntity = await this.userAccessRepository.GetUserAccess(userId);

            if(userAccessEntity == null)
            {
                accessOutputDto.UserAccess = new UserAccessOutputDto() { UserId = userId };
            }
            else
            {
                UserAccessOutputDto userAccessOutputDto = this.mapper.Map<UserAccessOutputDto>(userAccessEntity);
                accessOutputDto.UserAccess = userAccessOutputDto;
            }

            return accessOutputDto;
        }

        public async Task UpdateAllAccess(AccessOutputDto accessOutputDto)
        {
            UserAccessEntity userAccessEntity = this.mapper.Map<UserAccessEntity>(accessOutputDto.UserAccess);
            await this.userAccessRepository.UpdateUserAccess(userAccessEntity);
        }
    }
}

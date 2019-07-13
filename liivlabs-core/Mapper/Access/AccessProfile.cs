using AutoMapper;
using liivlabs_shared.DTO;
using liivlabs_shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace liivlabs_core.Mapper.Access
{
    public class AccessProfile: Profile
    {
        public AccessProfile()
        {
            CreateMap<UserAccessEntity, UserAccessOutputDto>();
            CreateMap<UserAccessOutputDto, UserAccessEntity>();
        }
    }
}

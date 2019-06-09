using AutoMapper;
using liivlabs_shared.DTO.Account;
using liivlabs_shared.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// User Profile to map
/// between different User DTO and
/// entities
/// </summary>
namespace liivlabs_core.Mapper.Account
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationInputDTO, UserEntity>();
            CreateMap<UserEntity, UserRegistrationOutputDTO>();
        }
    }
}

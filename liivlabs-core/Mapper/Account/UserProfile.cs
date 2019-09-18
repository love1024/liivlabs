using AutoMapper;
using liivlabs_shared;
using liivlabs_shared.DTO;
using liivlabs_shared.DTO.Account;
using liivlabs_shared.DTO.User;
using liivlabs_shared.Entities.Account;
using liivlabs_shared.Entities.File;
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
            CreateMap<UserEntity, UserOutoutDTO>();
            CreateMap<UserUpdateInputDTO, UserEntity>();
            CreateMap<UserEntity, UserEntity>();
            CreateMap<UserAddInputDTO, UserEntity>();
            CreateMap<FileEntity, FileOutputDTO>();
        }
    }
}

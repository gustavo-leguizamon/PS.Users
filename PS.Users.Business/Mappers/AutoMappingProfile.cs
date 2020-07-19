using AutoMapper;
using PS.Users.Domain.Entities;
using PS.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Users.Business.Mappers
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<User, UserModel>();

            CreateMap<RegisterModel, User>();
        }
    }
}

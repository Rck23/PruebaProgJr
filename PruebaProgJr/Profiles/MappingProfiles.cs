﻿using API.Dtos;
using AutoMapper;
using Core.Entities;


namespace API.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Fail, FailDto>()
                .ReverseMap();

        }
    }
}

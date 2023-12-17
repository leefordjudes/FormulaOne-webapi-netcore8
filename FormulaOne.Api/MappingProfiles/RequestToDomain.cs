﻿using AutoMapper;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests;

namespace FormulaOne.Api.MappingProfiles;

public class RequestToDomain : Profile
{
    public RequestToDomain()
    {
        CreateMap<CreateDriverAchievementRequest, Achievement>()
            .ForMember(
                dest => dest.RaceWins,
                opt => opt.MapFrom(src => src.Wins)
            )
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.AddedDate,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => DateTime.Now));
        
        CreateMap<UpdateDriverAchievementRequest, Achievement>()
            .ForMember(
                dest => dest.RaceWins,
                opt => opt.MapFrom(src => src.Wins)
            )
            .ForMember(dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => DateTime.Now));
        
        CreateMap<CreateDriverRequest, Driver>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.AddedDate,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => DateTime.Now));
        
        
        CreateMap<UpdateDriverRequest, Driver>()
            .ForMember(dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => DateTime.Now));

    }
}
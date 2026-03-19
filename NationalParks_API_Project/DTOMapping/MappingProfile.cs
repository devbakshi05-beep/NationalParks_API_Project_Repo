using AutoMapper;
using NationalParks_API_Project.Models;
using NationalParks_API_Project.Models.DTOs;

namespace NationalParks_API_Project.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<NationalParkDto,NationalPark>().ReverseMap();
            CreateMap<Trail,TrailDto>().ReverseMap();
        }
    }
}
//DB---MODEL---REPOSITORY---DTO---CLIENT
//CLIENT---DTO---RREPOSITORY---MODEL---DB

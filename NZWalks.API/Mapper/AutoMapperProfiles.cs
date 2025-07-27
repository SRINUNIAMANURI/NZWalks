using AutoMapper;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Model.InPutDto;

namespace NZWalks.API.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Region, RegionDto>();
            //CreateMap<Region, RegionInputDto>().ReverseMap();
            //CreateMap<Walk, WalkDto>().ReverseMap();
            //CreateMap<Walk, WalkInputDto>().ReverseMap();
        }        
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatfromService;

namespace PlatformService.Profiles
{
	public class PlatformsProfile : Profile
	{
        public PlatformsProfile()
        {
          // Source -> Target
          CreateMap<Platform, PlatformReadDto>();
          CreateMap<PlatformCreateDto, Platform>();
          CreateMap<PlatformReadDto, PlatformPublishedDto>();
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(scr => scr.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(scr => scr.Publisher));
        }
    }
}

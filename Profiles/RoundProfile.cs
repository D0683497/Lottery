using AutoMapper;
using Lottery.Entities;
using Lottery.Models;

namespace Lottery.Profiles
{
    public class RoundProfile : Profile
    {
        public RoundProfile()
        {
            CreateMap<Round, RoundViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.RoundId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.RoundName))
                .ForMember(dest => dest.Complete,
                    opt => opt.MapFrom(src => src.RoundComplete))
                .ForMember(dest => dest.Prizes,
                    opt => opt.MapFrom(src => src.Prizes))
                .ForMember(dest => dest.Winners,
                    opt => opt.MapFrom(src =>src.Winners))
                .ForMember(dest => dest.Attendees,
                    opt => opt.MapFrom(src => src.Attendees));

            CreateMap<RoundAddAddViewModel, Round>()
                .ForMember(dest => dest.RoundName,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Prizes,
                    opt => opt.MapFrom(src => src.Prizes))
                .ForMember(dest => dest.Attendees,
                    opt => opt.MapFrom(src => src.Attendees));
        }
    }
}
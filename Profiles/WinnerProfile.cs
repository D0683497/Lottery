using AutoMapper;
using Lottery.Entities;
using Lottery.Models;

namespace Lottery.Profiles
{
    public class WinnerProfile : Profile
    {
        public WinnerProfile()
        {
            CreateMap<Winner, WinnerViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.WinnerId))
                .ForMember(dest => dest.Attendees,
                    opt => opt.MapFrom(src => src.Attendees))
                .ForMember(dest => dest.Prize,
                    opt => opt.MapFrom(src => src.Prize));

            CreateMap<WinnerAddViewModel, Winner>()
                .ForMember(dest => dest.Attendees,
                    opt => opt.MapFrom(src => src.Attendees))
                .ForMember(dest => dest.Prize,
                    opt => opt.MapFrom(src => src.Prize));
        }
    }
}
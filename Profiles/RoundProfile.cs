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
                    opt => opt.MapFrom(src => src.RoundName));

            CreateMap<RoundAddAddViewModel, Round>()
                .ForMember(dest => dest.RoundName,
                    opt => opt.MapFrom(src => src.Name));
        }
    }
}
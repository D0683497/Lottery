using AutoMapper;
using Lottery.Entities.Identity;
using Lottery.Models;

namespace Lottery.Mappers
{
    public class HomeProfile : Profile
    {
        public HomeProfile()
        {
            #region ApplicationUser 轉換成 ApplyViewModel

            CreateMap<ApplicationUser, ApplyViewModel>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Purpose,
                    opt => opt.Ignore());

            #endregion
        }
    }
}